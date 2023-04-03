using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordUi.Services
{
    public interface ISpeechToTextService
    {
        public Task<string> GetTranscription(byte[] rawData, string prompt, string fileName="audio.webm");
    }
}
