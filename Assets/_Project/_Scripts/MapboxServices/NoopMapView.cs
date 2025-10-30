using Cysharp.Threading.Tasks;

namespace MapboxServices
{
    public sealed class NoopMapView : IMapView
    {
        public UniTask InitializeAsync(string styleId, string accessToken) { return UniTask.CompletedTask; }
        public UniTask RenderGeoJsonAsync(MapLayer layer, string geoJson) { return UniTask.CompletedTask; }
        public UniTask FlyToAsync(float lat, float lon, float zoom, float bearing, float pitch) { return UniTask.CompletedTask; }
        public void Dispose() { }
    }
}
