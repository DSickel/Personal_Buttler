using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Buttler.Model
{
    public class DialogflowResult
    {
        [JsonProperty("resolvedQuery")]
        public string ResolvedQuery { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("parameters")]
        public Parameter Parameters { get; set; }

        [JsonProperty("fulfillment")]
        public SpokenAnswer Fulfillment { get; set; }

        [JsonProperty("score")]
        public string Score { get; set; }
    }
}
