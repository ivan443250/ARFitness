using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapboxServices.Models
{
    public sealed class MapboxDirectionsResult
    {
        [JsonProperty("routes")] public List<MapboxRoute> Routes { get; set; }
    }
}
