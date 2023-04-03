using Microsoft.Extensions.Logging;

using OpenAiWhisperDemoApp.Services;

using RecordUi.Services;

namespace OpenAiWhisperDemoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    handlers.AddHandler<Microsoft.AspNetCore.Components.WebView.Maui.BlazorWebView, 
                                        OpenAiWhisperDemoApp.Platforms.Android.Handlers.MauiBlazorWebViewHandler>();
#endif
                });

            builder.Services.AddScoped<ISpeechToTextService, OpenAiWhisperService>();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}