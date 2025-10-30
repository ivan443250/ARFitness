using Cysharp.Threading.Tasks;
using System;

namespace MapboxServices
{
    public interface IMapView : IDisposable
    {
        UniTask InitializeAsync(string styleId, string accessToken);
        UniTask RenderGeoJsonAsync(MapLayer layer, string geoJson);
        UniTask FlyToAsync(float lat, float lon, float zoom, float bearing, float pitch);
    }
}
