using System.Net.Http.Headers;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using OpenAiWhisperDemo.Wasm.Client;
using OpenAiWhisperDemo.Wasm.Client.Services;

using RecordUi.Services;

using Serilog;

const string openApiKey = @"Use your OpenAI API Key";
Log.Logger = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Verbose()
#else
    .MinimumLevel.Information()
#endif
    .WriteTo.BrowserConsole()
    .CreateLogger();


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri(@"https://api.openai.com") };
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openApiKey);
    return client;
});

builder.Services.AddScoped<ISpeechToTextService, OpenAiWhisperService>();

await builder.Build().RunAsync();
