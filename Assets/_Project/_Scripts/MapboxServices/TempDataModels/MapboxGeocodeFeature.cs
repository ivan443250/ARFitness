using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapboxServices.Models
{
    public sealed class MapboxGeocodeFeature
    {
        [JsonProperty("place_name")] public string PlaceName { get; set; }
        [JsonProperty("center")] public List<double> Center { get; set; }
    }
}
