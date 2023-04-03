using System.Net.Http.Headers;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using OpenAiWhisperDemo.Wasm.Client;
using OpenAiWhisperDemo.Wasm.Client.Services;

using RecordUi.Services;


const string openApiKey = @"Use your OpenAI API Key";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
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
