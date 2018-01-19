using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Personal_Buttler.Model;
using Personal_Buttler.Service;
using Personal_Buttler.View;
using Plugin.TextToSpeech;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Personal_Buttler.ViewModel
{
    public class ButtlerPageViewModel : BaseViewModel
    {
        private string _text = "Jokes";

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private Weather _weather;

        public Weather Weather
        {
            get { return _weather; }
            set
            {
                _weather = value;
                OnPropertyChanged();
            }
        }

        public ButtlerPageViewModel()
        {
           // Title = "Personal Buttler Page ViewModel";
            Icon = "icon.png";
            System.Diagnostics.Debug.WriteLine("Binding Successful");
            SubscribeSpeechRecognition();
        }

        public Command OnSpeechInput_Clicked
        {
            get
            {
                return new Command(() =>
                {
                    //System.Diagnostics.Debug.WriteLine("Welcome to your personal Buttler");
                    //var page = new SpeechPopUpPage();
                    //PopupNavigation.PushAsync(page);
                    MessagingCenter.Send<ButtlerPageViewModel>(this, "SpeechInput");

                });
            }
        }

        private void SubscribeSpeechRecognition()
        {
            // TODO
            MessagingCenter.Subscribe<object, string>(this, "AIService_Result", (sender, result) =>
            {
                System.Diagnostics.Debug.WriteLine("Subscribe");
                AnalyseResultsFromDialogflow(result);
            });
        }


        private void AnalyseResultsFromDialogflow(string result)
        {
            try
            {
                APIClient client = APIClient.Instance;
                var jsonAction = JObject.Parse(result).ToString();
                var action = JsonConvert.DeserializeObject<DialogflowResult>(jsonAction);
                var parameter = action.Parameters;
                var fulfillment = action.Fulfillment;

                CrossTextToSpeech.Current.Speak(fulfillment.Speech);

                System.Diagnostics.Debug.WriteLine(fulfillment);

                switch (action.Action)
                {
                    case "input.wetter":
                        GetWeather(client, parameter);
                        break;

                    case "input.kleidung":


                        break;
                    case "input.kleidung.zusammenstellung":
                        break;
                    case "input.wetter-stimmung":
                        break;
                    case "input.witz.erzaehlen":
                        GetJoke(client);
                        break;
                }
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        private async void GetWeather(APIClient client, Parameter parameter)
        {
            try
            {
                var date = parameter.Date;

                int dateTime = -1;

                if (!date.Equals(""))
                {
                    dateTime = DateTime.Parse(date).Day;
                }

                int today = DateTime.Today.Day;

                if (date.Equals("") || dateTime == today)
                {
                    var weather = await client.SendGetRequestWeatherToday("https://api.openweathermap.org/data/2.5/weather", parameter.GeoCity);

                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    weather.Sunrise = start.AddSeconds(weather.Sys.Sunrise).ToLocalTime();
                    weather.Sunset = start.AddSeconds(weather.Sys.Sunset).ToLocalTime();

                    //var temp_Celsius = weather.Main.Temp - 273;
                    //var tempMax_Celsius = weather.Main.Temp_max - 273;
                    //var tempMin_Celsius = weather.Main.Temp_min - 273;

                    weather.Main.Temp -= 273;
                    weather.Main.Temp_max -= 273;
                    weather.Main.Temp_min -= 273;

                    Weather = weather;
                    CrossTextToSpeech.Current.Speak(weather.weather.Description + ", " + weather.Main.Temp + " Grad Celsius, Sunrise: " + weather.Sunrise + ",Sunset: " + weather.Sunset);

                }
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            
        }

        private async void GetJoke(APIClient client)
        {
            try
            {
                var joke = await client.SendGetRequestJoke("http://api.icndb.com/jokes/random");
                Text = joke;
                await CrossTextToSpeech.Current.Speak(joke);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }
    }
}
