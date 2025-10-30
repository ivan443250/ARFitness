using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapboxServices.Models
{
    public sealed class MapboxGeocodeResult
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("features")] public List<MapboxGeocodeFeature> Features { get; set; }
    }
}
