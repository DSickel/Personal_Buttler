using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Personal_Buttler.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Buttler.Service
{
    public class APIClient
    {
        private HttpClient restClient;
        public HttpClient RestClient { get; set; }
        private static APIClient instance;

        private const string QUERY_PARAM = "?q=";
        private const string APPID = "&appid=<API-KEY Openweathermap>";

        private APIClient()
        {
            CreateHttpClient();
        }

        public static APIClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new APIClient();
                }
                return instance;
            }
        }

        private void CreateHttpClient()
        {
            restClient = new HttpClient();
           // Uri endpoint = new Uri("http://api.icndb.com/jokes/");
           // restClient.BaseAddress = endpoint;
            RestClient = restClient;
        }

       public async Task<string> SendGetRequestJoke(string address)
       {
            System.Diagnostics.Debug.WriteLine("Test");
            var response = await restClient.GetAsync(address);
            var joke = "";
            if(response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var jobject = JObject.Parse(result).ToString();
                var json = JsonConvert.DeserializeObject<Witz>(result);
                System.Diagnostics.Debug.WriteLine(json.Value.Joke);
                joke = json.Value.Joke;
            }

            return joke;
       }

        public async Task<Weather> SendGetRequestWeatherToday(string address, string location)
        {
            System.Diagnostics.Debug.WriteLine("WeatherTest");

            //Query Parameter setzen
            //http://samples.openweathermap.org/data/2.5/weather => address

            Weather weather = null;
            try
            {
                var uri = new Uri("http://samples.openweathermap.org/data/2.5/weather?q=London,uk&appid=b6907d289e10d714a6e88b30761fae22");
                var endpoint = string.Concat(address, QUERY_PARAM, location, APPID);
                var uri2 = new Uri(endpoint);
                var response = await restClient.GetAsync(uri2); // Anfrage an Openweathermap Rest Service

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    // Wetterobjekt holen
                    var jarrayWeather = JObject.Parse(result)["weather"].ToString();
                    // var jobjectWeather = jarrayWeather.Children<JObject>().FirstOrDefault().ToString();                   
                    var jsonWeather = JsonConvert.DeserializeObject<List<WeatherObject>>(jarrayWeather);

                    // Mainobjekt holen
                    var jobjectMain = JObject.Parse(result)["main"].ToString();
                    var jsonMain = JsonConvert.DeserializeObject<Main>(jobjectMain);

                    // Wind holen
                    var jobjectWind = JObject.Parse(result)["wind"].ToString();
                    var jsonWind = JsonConvert.DeserializeObject<Wind>(jobjectWind);

                    // Sys holen
                    var jobjectSys = JObject.Parse(result)["sys"].ToString();
                    var jsonSys = JsonConvert.DeserializeObject<Sys>(jobjectSys);

                    if (jsonWeather.Count == 0)
                    {
                        jsonWeather.Add(new WeatherObject());
                    }

                    weather = new Weather(jsonWeather[0], jsonMain, jsonWind, jsonSys);

                  
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            return weather;
        }         

        public async Task<string> SendGetRequestWeatherForecast(string address, string location, string date)
        {
            System.Diagnostics.Debug.WriteLine("WeatherTest");

            //Query Parameter setzen
            //http://samples.openweathermap.org/data/2.5/forecast => address
            var endpoint = string.Concat(address, QUERY_PARAM, location, APPID);

            var response = await restClient.GetAsync(address);

            return "Test";
        }





    }
}
