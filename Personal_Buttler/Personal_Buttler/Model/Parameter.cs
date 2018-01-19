using Newtonsoft.Json;

namespace Personal_Buttler.Model
{
    public class Parameter
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("geo-city")]
        public string GeoCity { get; set; }

        [JsonProperty("Wetterbedingungen")]
        public string Wetterbedingungen { get; set; }
    }
}