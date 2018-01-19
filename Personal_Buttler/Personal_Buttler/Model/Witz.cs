using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Buttler.Model
{
    public class Witz
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")] 
        public Value Value { get; set; }
    }
}
