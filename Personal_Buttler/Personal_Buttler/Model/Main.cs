using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Buttler.Model
{
    public class Main
    {
        [JsonProperty("temp")]
        public float Temp { get; set; }

        [JsonProperty("temp_min")]
        public float Temp_min { get; set; }

        [JsonProperty("temp_max")]
        public float Temp_max { get; set; }
    }
}
