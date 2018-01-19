using Newtonsoft.Json;

namespace Personal_Buttler.Model
{
    public class Sys
    {
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }
}