using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapboxServices.Models
{
    public sealed class MapboxGeometry
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("coordinates")] public List<List<double>> Coordinates { get; set; }
    }
}
