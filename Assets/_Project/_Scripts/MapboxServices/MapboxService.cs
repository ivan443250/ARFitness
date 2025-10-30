using Cysharp.Threading.Tasks;
using DataModels;
using MapboxServices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace MapboxServices
{
    public sealed class MapboxService : IMapService
    {
        private string _accessToken;
        private string _styleId;
        private readonly IMapView _view;

        private const string GeocodingReverseTemplate = "https://api.mapbox.com/geocoding/v6/mapbox.places/{0},{1}.json?language={2}&access_token={3}";
        private const string DirectionsTemplate = "https://api.mapbox.com/directions/v5/mapbox/{0}/{1}?geometries=geojson&overview=full&steps=true&access_token={2}";
        private const string MapMatchingTemplate = "https://api.mapbox.com/matching/v5/mapbox/{0}/{1}?geometries=geojson&tidy=true&access_token={2}";
        private const string IsochroneTemplate = "https://api.mapbox.com/isochrone/v1/mapbox/{0}/{1},{2}?polygons=true&contours_minutes={3}&generalize={4}&access_token={5}";
        private const string StaticRasterTileTemplate = "https://api.mapbox.com/v4/{0}/{1}/{2}/{3}.pngraw?access_token={4}";

        public MapboxService(IMapView view)
        {
            if (view == null) throw new ArgumentNullException(nameof(view));
            _view = view;
            _styleId = "mapbox.satellite-streets-v12";
            _accessToken = string.Empty;
        }

        public void SetAccessToken(string token)
        {
            _accessToken = token ?? string.Empty;
        }

        public void SetStyleId(string styleId)
        {
            _styleId = string.IsNullOrWhiteSpace(styleId) ? _styleId : styleId;
        }

        public async UniTask InitializeAsync()
        {
            await _view.InitializeAsync(_styleId, _accessToken);
        }

        public async UniTask ShowRouteAsync(Route route)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            if (route.Checkpoints == null || route.Checkpoints.Count < 2) throw new ArgumentException("Route requires at least 2 checkpoints.");
            string geoJson = BuildLineStringGeoJsonFromRoute(route);
            await _view.RenderGeoJsonAsync(MapLayer.Route, geoJson);
        }

        public async UniTask ShowCheckpointsAsync(IReadOnlyList<Checkpoint> checkpoints)
        {
            if (checkpoints == null) throw new ArgumentNullException(nameof(checkpoints));
            string geoJsonPoints = BuildPointsGeoJsonFromCheckpoints(checkpoints);
            await _view.RenderGeoJsonAsync(MapLayer.Checkpoints, geoJsonPoints);
            string geoJsonCircles = BuildCirclesGeoJsonFromCheckpoints(checkpoints, 32);
            await _view.RenderGeoJsonAsync(MapLayer.Radius, geoJsonCircles);
        }

        public async UniTask FlyToAsync(GeoPoint center, float zoom, float bearing = 0f, float pitch = 0f)
        {
            await _view.FlyToAsync(center.Latitude, center.Longitude, zoom, bearing, pitch);
        }

        public async UniTask<MapboxGeocodeResult> ReverseGeocodeAsync(GeoPoint point, string language = "en")
        {
            string lat = point.Latitude.ToString(CultureInfo.InvariantCulture);
            string lon = point.Longitude.ToString(CultureInfo.InvariantCulture);
            string url = string.Format(CultureInfo.InvariantCulture, GeocodingReverseTemplate, lon, lat, language, _accessToken);
            string json = await HttpGetAsync(url);
            MapboxGeocodeResult result = JsonConvert.DeserializeObject<MapboxGeocodeResult>(json);
            return result;
        }

        public async UniTask<MapboxDirectionsResult> GetDirectionsAsync(IReadOnlyList<GeoPoint> waypoints, MapboxProfile profile)
        {
            if (waypoints == null || waypoints.Count < 2) throw new ArgumentException("At least 2 waypoints required.");
            string coords = BuildLonLatPath(waypoints);
            string prof = ConvertProfile(profile);
            string url = string.Format(CultureInfo.InvariantCulture, DirectionsTemplate, prof, coords, _accessToken);
            string json = await HttpGetAsync(url);
            MapboxDirectionsResult result = JsonConvert.DeserializeObject<MapboxDirectionsResult>(json);
            return result;
        }

        public async UniTask<MapboxMatchResult> MatchTrackAsync(IReadOnlyList<GeoPoint> trace, MapboxProfile profile)
        {
            if (trace == null || trace.Count < 2) throw new ArgumentException("At least 2 points required.");
            string coords = BuildLonLatPath(trace);
            string prof = ConvertProfile(profile);
            string url = string.Format(CultureInfo.InvariantCulture, MapMatchingTemplate, prof, coords, _accessToken);
            string json = await HttpGetAsync(url);
            MapboxMatchResult result = JsonConvert.DeserializeObject<MapboxMatchResult>(json);
            return result;
        }

        public async UniTask<MapboxIsochroneResult> GetIsochroneAsync(GeoPoint center, MapboxProfile profile, int minutes, int generalizeMeters = 50)
        {
            string prof = ConvertProfile(profile);
            string lon = center.Longitude.ToString(CultureInfo.InvariantCulture);
            string lat = center.Latitude.ToString(CultureInfo.InvariantCulture);
            string url = string.Format(CultureInfo.InvariantCulture, IsochroneTemplate, prof, lon, lat, minutes, generalizeMeters, _accessToken);
            string json = await HttpGetAsync(url);
            MapboxIsochroneResult result = JsonConvert.DeserializeObject<MapboxIsochroneResult>(json);
            return result;
        }

        public async UniTask PreloadTilesAsync(Route route, int minZoom, int maxZoom)
        {
            if (route == null) throw new ArgumentNullException(nameof(route));
            BoundingBox bbox = ComputeRouteBoundingBox(route);
            for (int z = minZoom; z <= maxZoom; z++)
            {
                List<Vector2Int> tiles = ComputeTilesForBoundingBox(bbox, z);
                for (int i = 0; i < tiles.Count; i++)
                {
                    Vector2Int t = tiles[i];
                    string url = string.Format(CultureInfo.InvariantCulture, StaticRasterTileTemplate, _styleId, z, t.x, t.y, _accessToken);
                    await WarmCacheAsync(url);
                }
            }
        }

        public void Dispose()
        {
            _view.Dispose();
        }

        private static string ConvertProfile(MapboxProfile profile)
        {
            if (profile == MapboxProfile.Walking) return "walking";
            if (profile == MapboxProfile.Cycling) return "cycling";
            if (profile == MapboxProfile.Driving) return "driving";
            return "driving-traffic";
        }

        private static string BuildLonLatPath(IReadOnlyList<GeoPoint> points)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < points.Count; i++)
            {
                if (i > 0) sb.Append(";");
                string lon = points[i].Longitude.ToString(CultureInfo.InvariantCulture);
                string lat = points[i].Latitude.ToString(CultureInfo.InvariantCulture);
                sb.Append(lon).Append(",").Append(lat);
            }
            return sb.ToString();
        }

        private static string BuildLineStringGeoJsonFromRoute(Route route)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"type\":\"FeatureCollection\",\"features\":[{\"type\":\"Feature\",\"geometry\":{\"type\":\"LineString\",\"coordinates\":[");
            for (int i = 0; i < route.Checkpoints.Count; i++)
            {
                if (i > 0) sb.Append(",");
                string lon = route.Checkpoints[i].Location.Longitude.ToString(CultureInfo.InvariantCulture);
                string lat = route.Checkpoints[i].Location.Latitude.ToString(CultureInfo.InvariantCulture);
                sb.Append("[").Append(lon).Append(",").Append(lat).Append("]");
            }
            sb.Append("]}}]}");
            return sb.ToString();
        }

        private static string BuildPointsGeoJsonFromCheckpoints(IReadOnlyList<Checkpoint> points)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"type\":\"FeatureCollection\",\"features\":[");
            for (int i = 0; i < points.Count; i++)
            {
                if (i > 0) sb.Append(",");
                string lon = points[i].Location.Longitude.ToString(CultureInfo.InvariantCulture);
                string lat = points[i].Location.Latitude.ToString(CultureInfo.InvariantCulture);
                sb.Append("{\"type\":\"Feature\",\"geometry\":{\"type\":\"Point\",\"coordinates\":[")
                  .Append(lon).Append(",").Append(lat).Append("]}}");
            }
            sb.Append("]}");
            return sb.ToString();
        }

        private static string BuildCirclesGeoJsonFromCheckpoints(IReadOnlyList<Checkpoint> centers, int segments)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"type\":\"FeatureCollection\",\"features\":[");
            for (int i = 0; i < centers.Count; i++)
            {
                if (i > 0) sb.Append(",");
                string poly = BuildCirclePolygon(centers[i].Location, centers[i].RadiusMeters, segments);
                sb.Append(poly);
            }
            sb.Append("]}");
            return sb.ToString();
        }

        private static string BuildCirclePolygon(GeoPoint center, float radiusMeters, int segments)
        {
            const float EarthRadius = 6378137f;
            float latRad = center.Latitude * Mathf.Deg2Rad;
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"type\":\"Feature\",\"geometry\":{\"type\":\"Polygon\",\"coordinates\":[[");
            for (int i = 0; i <= segments; i++)
            {
                if (i > 0) sb.Append(",");
                float theta = (2f * Mathf.PI * i) / segments;
                float dx = radiusMeters * Mathf.Cos(theta);
                float dy = radiusMeters * Mathf.Sin(theta);
                float lat2 = (float)(center.Latitude + (dy / EarthRadius) * (180.0 / Math.PI));
                float lon2 = (float)(center.Longitude + (dx / (EarthRadius * Math.Cos(latRad))) * (180.0 / Math.PI));
                string lon = lon2.ToString(CultureInfo.InvariantCulture);
                string lat = lat2.ToString(CultureInfo.InvariantCulture);
                sb.Append("[").Append(lon).Append(",").Append(lat).Append("]");
            }
            sb.Append("]]}}");
            return sb.ToString();
        }

        private static BoundingBox ComputeRouteBoundingBox(Route route)
        {
            float minLat = float.PositiveInfinity;
            float minLon = float.PositiveInfinity;
            float maxLat = float.NegativeInfinity;
            float maxLon = float.NegativeInfinity;

            for (int i = 0; i < route.Checkpoints.Count; i++)
            {
                float lat = route.Checkpoints[i].Location.Latitude;
                float lon = route.Checkpoints[i].Location.Longitude;
                if (lat < minLat) minLat = lat;
                if (lon < minLon) minLon = lon;
                if (lat > maxLat) maxLat = lat;
                if (lon > maxLon) maxLon = lon;
            }

            float padLat = (maxLat - minLat) * 0.1f + 0.0005f;
            float padLon = (maxLon - minLon) * 0.1f + 0.0005f;

            BoundingBox bbox = new BoundingBox
            {
                MinLat = minLat - padLat,
                MinLon = minLon - padLon,
                MaxLat = maxLat + padLat,
                MaxLon = maxLon + padLon
            };
            return bbox;
        }

        private static List<Vector2Int> ComputeTilesForBoundingBox(BoundingBox bbox, int zoom)
        {
            List<Vector2Int> result = new List<Vector2Int>();
            int xMin = LonToTileX(bbox.MinLon, zoom);
            int xMax = LonToTileX(bbox.MaxLon, zoom);
            int yMin = LatToTileY(bbox.MaxLat, zoom);
            int yMax = LatToTileY(bbox.MinLat, zoom);
            for (int x = xMin; x <= xMax; x++)
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    result.Add(new Vector2Int(x, y));
                }
            }
            return result;
        }

        private static int LonToTileX(float lon, int zoom)
        {
            double x = (lon + 180.0) / 360.0 * (1 << zoom);
            int xi = (int)Math.Floor(x);
            int max = (1 << zoom) - 1;
            return Mathf.Clamp(xi, 0, max);
        }

        private static int LatToTileY(float lat, int zoom)
        {
            double latRad = lat * Math.PI / 180.0;
            double n = Math.PI - Math.Log(Math.Tan(Math.PI / 4.0 + latRad / 2.0));
            double y = (n / Math.PI) / 2.0 * (1 << zoom);
            int yi = (int)Math.Floor(y);
            int max = (1 << zoom) - 1;
            return Mathf.Clamp(yi, 0, max);
        }

        private static async UniTask<string> HttpGetAsync(string url)
        {
            using (UnityWebRequest req = UnityWebRequest.Get(url))
            {
                UnityWebRequestAsyncOperation op = req.SendWebRequest();
                while (!op.isDone) await UniTask.Yield();
#if UNITY_2020_2_OR_NEWER
                if (req.result != UnityWebRequest.Result.Success) throw new Exception(req.error);
#else
                if (req.isNetworkError || req.isHttpError) throw new Exception(req.error);
#endif
                string json = req.downloadHandler.text;
                return json;
            }
        }

        private static async UniTask WarmCacheAsync(string url)
        {
            using (UnityWebRequest req = UnityWebRequest.Get(url))
            {
                UnityWebRequestAsyncOperation op = req.SendWebRequest();
                while (!op.isDone) await UniTask.Yield();
            }
        }
    }
}
