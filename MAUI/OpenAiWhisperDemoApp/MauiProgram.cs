using Microsoft.Extensions.Logging;

using OpenAiWhisperDemoApp.Services;

using RecordUi.Services;

using Serilog;

namespace OpenAiWhisperDemoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Verbose() 
#endif
#if ANDROID
                .Enrich.WithProperty("AppName","OpenAiWhisperDemoApp")
                .WriteTo.AndroidLog(outputTemplate: "_{AppName}_ [{Level}] {Message:l{NewLine:l}{Exception:l}")

#elif IOS || MACCATALYST
                .WriteTo.NSLog()

#else
                .WriteTo.Debug()
#endif
                .CreateLogger();

            var builder = MauiApp.CreateBuilder();
            builder.Services.AddLogging(loggingBuilder =>
            {
#if IOS || MACCATALYST
                loggingBuilder.ClearProviders();
#endif
                loggingBuilder.AddSerilog(dispose: true);
            });

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