using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Personal_Buttler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Personal_Buttler.ViewModel
{
    public class SpeechPopUpViewModel : BaseViewModel
    {
        private string _isListening;

        public string IsListening
        {
            get { return _isListening; }
            set
            {
                _isListening = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public Command CancelSpeechInput
        {
            get
            {
                return new Command(() =>
                {
                    MessagingCenter.Send<SpeechPopUpViewModel>(this, "Cancel");
                });
            }
        }

        #endregion

        public SpeechPopUpViewModel()
        {
            IsBusy = true;
            IsListening = "Listening...";

            SubscribeSpeechRecognition();
            SendSpeechCommand();
        }


        private void SendSpeechCommand()
        {
            MessagingCenter.Send<SpeechPopUpViewModel>(this, "SpeechInput");
        }

        private void SendNavigationRequest(BaseViewModel viewModel)
        {
            MessagingCenter.Send<string, object>("test", "navigation", viewModel);
        }

        #region MessagingCenter Subscription

        private void SubscribeSpeechRecognition()
        {
            // TODO
            MessagingCenter.Subscribe<object, string>(this, "AIService_Result", (sender, result) =>
            {
                System.Diagnostics.Debug.WriteLine("Subscribe");
                AnalyseResultsFromDialogflow(result);
            });
        }

        #endregion

        private void AnalyseResultsFromDialogflow(string result)
        {
            var jsonAction = JObject.Parse(result)["result"].ToString();
            var action = JsonConvert.DeserializeObject<DialogflowResult>(jsonAction);
            var parameter = action.Parameters;
            var fulfillment = action.Fulfillment;

            System.Diagnostics.Debug.WriteLine(fulfillment);

            switch (action.Action)
            {
                case "input.wetter":
                    break;

                case "input.kleidung":
                    break;
                case "input.kleidung.zusammenstellung":
                    break;
                case "input.wetter-stimmung":
                    break;
                case "input.witz.erzaehlen":
                    break;
            }
        }

        #region MessagingCenter Unsubscriptions

        private void UnSubscribeSpeechRecognition()
        {
            MessagingCenter.Unsubscribe<object, string>(this, "AIService_Result");
        }

        #endregion
    }
}
