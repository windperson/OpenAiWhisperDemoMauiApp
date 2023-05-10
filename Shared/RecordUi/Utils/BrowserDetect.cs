using DeviceDetectorNET;
using DeviceDetectorNET.Parser;

namespace RecordUi.Utils;

public class BrowserDetect
{
    public static bool RunningOnIos(string userAgent)
    {
        DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_NONE);
        var detector = new DeviceDetector(userAgent);
        detector.SkipBotDetection();
        detector.Parse();

        var osName = string.Empty;
        var osInfo = detector.GetOs();
        if (osInfo.Success)
        {
            osName = osInfo.Match.Name;
        }

        var browserName = string.Empty;
        var browserEngine = string.Empty;
        var browserInfo = detector.GetBrowserClient();
        if (browserInfo.Success)
        {
            browserName = browserInfo.Match.Name;
            browserEngine = browserInfo.Match.Engine;
        }

        return osName.ToLower().Contains("ios") && browserEngine.ToLower().Contains("webkit") || browserName.ToLower().Contains("safari");
    }

}