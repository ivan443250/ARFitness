using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapboxServices.Models
{
    public sealed class MapboxIsochroneResult
    {
        [JsonProperty("features")] public List<MapboxGeocodeFeature> Features { get; set; }
    }
}
