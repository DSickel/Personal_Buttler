using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Buttler.Model
{
    public class Weather
    {

        public Weather(WeatherObject weather, Main main, Wind wind, Sys sys)
        {
            this.weather = weather;
            this.Main = main;
            this.Wind = wind;
            this.Sys = sys;
        }

        [JsonProperty("weather")]
        public WeatherObject weather { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
    }
}
