using Newtonsoft.Json;

namespace MapboxServices.Models
{
    public sealed class MapboxRoute
    {
        [JsonProperty("distance")] public double Distance { get; set; }
        [JsonProperty("duration")] public double Duration { get; set; }
        [JsonProperty("geometry")] public MapboxGeometry Geometry { get; set; }
    }
}
