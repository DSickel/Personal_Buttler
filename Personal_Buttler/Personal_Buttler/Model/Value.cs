
using Newtonsoft.Json;

namespace Personal_Buttler.Model
{
    public class Value
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("joke")]
        public string Joke { get; set; }
    }
}