using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapboxServices.Models
{
    public sealed class MapboxTracePoint
    {
        [JsonProperty("alternatives_count")] public int AlternativesCount { get; set; }
        [JsonProperty("matchings_index")] public int? MatchingsIndex { get; set; }
        [JsonProperty("waypoint_index")] public int WaypointIndex { get; set; }
        [JsonProperty("location")] public List<double> Location { get; set; }
    }
}
