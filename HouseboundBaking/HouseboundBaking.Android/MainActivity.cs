using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;

namespace HouseboundBaking.Droid
{
    [Activity(Label = "HouseboundBaking", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //IMicrophoneService micService;
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            //Plugin for popUp
            Rg.Plugins.Popup.Popup.Init(this);
            //https://github.com/rotorgames/Rg.Plugins.Popup/wiki/Getting-started

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            //   micService = DependencyService.Resolve<IMicrophoneService>();

            //SMSBroadcastReceiver receiver = new SMSBroadcastReceiver();
            Intent intent = new Intent(this, typeof(SMSBroadcastReceiver));
            StartService(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //NOTE microphone 

            switch (requestCode)
            {
                //case AndroidMicrophoneService.RecordAudioPermissionCode:
                //    if (grantResults[0] == Permission.Granted)
                //    {
                //        micService.OnRequestPermissionResult(true);
                //    }
                //    else
                //    {
                //        micService.OnRequestPermissionResult(false);
                //    }
                //    break;
            }
        }
    }
}