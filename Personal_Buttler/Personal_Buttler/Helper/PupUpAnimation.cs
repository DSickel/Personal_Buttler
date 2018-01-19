using Rg.Plugins.Popup.Interfaces.Animations;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Personal_Buttler.Helper
{
    public class PupUpAnimation : IPopupAnimation
    {
        // Call Before OnAppering
        public void Preparing(Xamarin.Forms.View content, PopupPage page)
        {
            // Preparing content and page
            content.Opacity = 0;
        }

        // Call After OnDisappering
        public void Disposing(Xamarin.Forms.View content, PopupPage page)
        {
            // Dispose Unmanaged Code
        }

        // Call After OnAppering
        public async Task Appearing(Xamarin.Forms.View content, PopupPage page)
        {
            var topOffset = GetTopOffset(content, page);
            content.TranslationY = topOffset;
            content.Opacity = 1;

            await content.TranslateTo(0, 0, easing: Easing.CubicOut);
        }

        // Call Before OnDisappering
        public async Task Disappearing(Xamarin.Forms.View content, PopupPage page)
        {
            // Hide animation
            await content.FadeTo(0);
        }

        private int GetTopOffset(Xamarin.Forms.View content, Page page)
        {
            return (int)(content.Height + page.Height) / 2;
        }
    }
}
