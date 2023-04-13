﻿using Microsoft.AspNetCore.Components.WebView;

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

            if (microPhonePermission != PermissionStatus.Granted)
            {
                await DisplayAlert("Permission Denied", "You must grant permission to use the microphone", "OK");
            }
#else
            var microPhonePermission = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microPhonePermission != PermissionStatus.Granted)
            {
                microPhonePermission = await Permissions.RequestAsync<Permissions.Microphone>();
            }

            if (microPhonePermission != PermissionStatus.Granted)
            {
                await DisplayAlert("Permission Denied", "You must grant permission to use the microphone", "OK");
            }
#endif
        }
    }
}