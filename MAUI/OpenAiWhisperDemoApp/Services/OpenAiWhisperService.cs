using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using RecordUi.Services;

namespace OpenAiWhisperDemoApp.Services
{
    public class OpenAiWhisperService : ISpeechToTextService
    {
        const string openApiKey = @"Use your OpenAI API Key";

        public async Task<string> GetTranscription(byte[] rawData, string prompt, string fileName = "audio.webm")
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return "{Error: \"Internet access unavailable\"";
            }

            using var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(rawData), "file", fileName);
            content.Add(new StringContent("whisper-1"), "model");
            if (!string.IsNullOrEmpty(prompt))
            {
                content.Add(new StringContent(prompt), "prompt");
            }

            using var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(@"https://api.openai.com") 
            }; 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openApiKey);

            var response = await httpClient.PostAsync("v1/audio/transcriptions", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }
    }
}
