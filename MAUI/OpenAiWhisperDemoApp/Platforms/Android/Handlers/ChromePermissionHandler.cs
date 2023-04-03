using Android.Webkit;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace OpenAiWhisperDemoApp.Platforms.Android.Handlers
{
    internal class CustomWebChromeClient : WebChromeClient
    {
        public override void OnPermissionRequest(PermissionRequest request)
        {
            // brute force implementation of Grand Android Permission,
            // see: https://stackoverflow.com/a/75591585 
            try
            {
                request.Grant(request.GetResources());
                base.OnPermissionRequest(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public class MauiBlazorWebViewHandler : BlazorWebViewHandler
    {
        protected override global::Android.Webkit.WebView CreatePlatformView()
        {
            var view = base.CreatePlatformView();

            view.SetWebChromeClient(new CustomWebChromeClient());

            return view;
        }
    }
}
