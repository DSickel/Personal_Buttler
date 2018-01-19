using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ApiAiSDK;
using ApiAi.Common;
using Xamarin.Forms;
using ApiAi.Android;
using Personal_Buttler.ViewModel;
using Personal_Buttler.Helper;
using ApiAiSDK.Model;
using Newtonsoft.Json;

namespace Personal_Buttler.Droid.Model
{
    public class AIServiceButtler
    {
        private AIConfiguration _config;
        private AIService _aiService;
        private Activity _mainActivity;

        public AIServiceButtler(Activity mainActivity)
        {
            _config = new AIConfiguration("<API-KEY Dialogflow>", SupportedLanguage.German);
            _aiService = AIService.CreateService(Forms.Context, _config, RecognitionEngine.System);
            _mainActivity = mainActivity;

            _aiService.OnResult += _aiService_OnResult;
            _aiService.OnError += _aiService_OnError;

            // SubscribeSpeechPopUpViewModel();
            SubscribeForStart();
            SubscribeCancelRequest();
        }


        private void _aiService_OnError(AIServiceException error)
        {
            System.Diagnostics.Debug.WriteLine("Error during Speech Input");
        }

        private void _aiService_OnResult(AIResponse response)
        {
            System.Diagnostics.Debug.WriteLine("We got a Response from Api.ai");

            _mainActivity.RunOnUiThread(() =>
            {
                if (response.IsError)
                {
                    if (response.Result != null)
                    {
                        System.Diagnostics.Debug.WriteLine("AIServiceTest: " + response.Result.Action);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("AIServiceTest: " + response.Result.Action);
                    }
                }
                else
                {
                    // Send Close Message to PopUpPage 
                    //SendClosePopUp();

                    var result = response.Result;
                    string json = JsonConvert.SerializeObject(result);

                   SendResults(json);
                }
            });

        }

        private void SubscribeForStart()
        {
            MessagingCenter.Subscribe<ButtlerPageViewModel>(this, "SpeechInput", (sender) =>
            {
                StartApiAiService();
            });
        }


        private void SendClosePopUp()
        {
            MessagingCenter.Send<AiServiceHelper>(new AiServiceHelper(), "ClosePopUp");
        }

        private void SendResults(string results)
        {
            MessagingCenter.Send<object, string>(this, "AIService_Result", results);
        }

        #region Start / Stop Listening

        public void StartApiAiService()
        {
            _aiService.StartListening();
        }

        public void StopApiAiService()
        {
            _aiService.StopListening();
            //SendClosePopUp();
        }

        #endregion

        #region MessaginCenter Subscribe

        public void SubscribeSpeechPopUpViewModel()
        {
            MessagingCenter.Subscribe<SpeechPopUpViewModel>(this, "SpeechInput", (sender) =>
            {
                StartApiAiService();
            });
        }

        public void SubscribeCancelRequest()
        {
            MessagingCenter.Subscribe<SpeechPopUpViewModel>(this, "Cancel", (sender) =>
            {
                StopApiAiService();
            });
        }

        #endregion
    }
}