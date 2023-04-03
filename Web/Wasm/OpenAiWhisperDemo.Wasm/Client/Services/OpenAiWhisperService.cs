using RecordUi.Services;

namespace OpenAiWhisperDemo.Wasm.Client.Services;

public class OpenAiWhisperService : ISpeechToTextService
{
    private readonly HttpClient _httpClient;

    public OpenAiWhisperService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetTranscription(byte[] rawData, string prompt, string fileName = "audio.webm")
    {
        using var content = new MultipartFormDataContent();
        content.Add(new ByteArrayContent(rawData), "file", fileName);
        content.Add(new StringContent("whisper-1"), "model");
        if (!string.IsNullOrEmpty(prompt))
        {
            content.Add(new StringContent(prompt), "prompt");
        }

        var response = await _httpClient.PostAsync("v1/audio/transcriptions", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
