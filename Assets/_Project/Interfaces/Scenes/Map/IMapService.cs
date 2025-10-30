using Cysharp.Threading.Tasks;
using DataModels;
using MapboxServices.Models;
using System;
using System.Collections.Generic;

namespace MapboxServices
{
    public interface IMapService : IDisposable
    {
        void SetAccessToken(string token);
        void SetStyleId(string styleId);
        UniTask InitializeAsync();
        UniTask ShowRouteAsync(Route route);
        UniTask ShowCheckpointsAsync(IReadOnlyList<Checkpoint> checkpoints);
        UniTask FlyToAsync(GeoPoint center, float zoom, float bearing = 0f, float pitch = 0f);
        UniTask<MapboxGeocodeResult> ReverseGeocodeAsync(GeoPoint point, string language = "en");
        UniTask<MapboxDirectionsResult> GetDirectionsAsync(IReadOnlyList<GeoPoint> waypoints, MapboxProfile profile);
        UniTask<MapboxMatchResult> MatchTrackAsync(IReadOnlyList<GeoPoint> trace, MapboxProfile profile);
        UniTask<MapboxIsochroneResult> GetIsochroneAsync(GeoPoint center, MapboxProfile profile, int minutes, int generalizeMeters = 50);
        UniTask PreloadTilesAsync(Route route, int minZoom, int maxZoom);
    }
}
