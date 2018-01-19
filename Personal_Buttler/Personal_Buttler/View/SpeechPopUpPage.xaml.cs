using Personal_Buttler.Helper;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Personal_Buttler.View
{
    public partial class SpeechPopUpPage : PopupPage
    {
        public SpeechPopUpPage()
        {
            InitializeComponent();
            Animation = new PupUpAnimation();           
        }

        private void SubscribeToAiServiceResult()
        {
            MessagingCenter.Subscribe<AiServiceHelper>(this, "ClosePopUp", (sender) =>
            {
                System.Diagnostics.Debug.WriteLine("SpeechPopup: ClosePopUpMessage: " + this.GetHashCode());
                ClosePage();
            });
        }

        private void UnsubscribeToAiServiceResult()
        {
            MessagingCenter.Unsubscribe<AiServiceHelper>(this, "ClosePopUp");
        }

        private async void ClosePage()
        {
            await Navigation.PopPopupAsync();
        }

        protected override void OnAppearing()
        {
            SubscribeToAiServiceResult();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            UnsubscribeToAiServiceResult();
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            //return base.OnBackButtonPressed();
            return true;
        }
    }
}
