using Newtonsoft.Json;

namespace Personal_Buttler.Model
{
    public class SpokenAnswer
    {
        [JsonProperty("speech")]
        public string Speech { get; set; }
    }
}