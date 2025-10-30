using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapboxServices.Models
{
    public sealed class MapboxMatchResult
    {
        [JsonProperty("matchings")] public List<MapboxRoute> Matchings { get; set; }
        [JsonProperty("tracepoints")] public List<MapboxTracePoint> Tracepoints { get; set; }
    }
}
