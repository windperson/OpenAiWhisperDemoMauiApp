using Android;

namespace OpenAiWhisperDemoApp.Platforms.Android.AppPermissions
{
    public class BlazorRecordAudioPermissions : Permissions.BasePlatformPermission
    {
        public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new[]
        {
            (Manifest.Permission.RecordAudio, true),
            (Manifest.Permission.ModifyAudioSettings, true)
        };
    }
}
