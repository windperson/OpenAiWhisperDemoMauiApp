using Microsoft.AspNetCore.Components.WebView;

#if ANDROID
using OpenAiWhisperDemoApp.Platforms.Android.AppPermissions;
#endif

namespace OpenAiWhisperDemoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
#if ANDROID
            var microPhonePermission = await Permissions.CheckStatusAsync<BlazorRecordAudioPermissions>();
            if (microPhonePermission != PermissionStatus.Granted)
            {
                microPhonePermission = await Permissions.RequestAsync<BlazorRecordAudioPermissions>();
            }
#else
            var microPhonePermission = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microPhonePermission != PermissionStatus.Granted)
            {
                microPhonePermission = await Permissions.RequestAsync<Permissions.Microphone>();
            }
#endif
            if (microPhonePermission == PermissionStatus.Granted)
            {
                return;
            }
            await DisplayAlert("Permission Denied", "You must grant permission to use microphone for voice recording", "OK");
            blazorWebView.RootComponents.Clear();
        }
    }
}