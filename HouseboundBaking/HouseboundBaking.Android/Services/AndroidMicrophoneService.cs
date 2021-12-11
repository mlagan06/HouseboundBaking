using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HouseboundBaking.Droid.Services;
using HouseboundBaking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidMicrophoneService))]
namespace HouseboundBaking.Droid.Services
{
    public class AndroidMicrophoneService : IMicrophoneService
    {
        //public const int RecordAudioPermissionCode = 1;
        //private TaskCompletionSource<bool> tcsPermissions;
        //string[] permissions = new string[] { Manifest.Permission.RecordAudio };

        //public Task<bool> GetPermissionAsync()
        //{
        //    tcsPermissions = new TaskCompletionSource<bool>();

        //    if ((int)Build.VERSION.SdkInt < 23)
        //    {
        //        tcsPermissions.TrySetResult(true);
        //    }
        //    else
        //    {
        //        var currentActivity = MainActivity.Instance;
        //        if (ActivityCompat.CheckSelfPermission(currentActivity, Manifest.Permission.RecordAudio) != (int)Permission.Granted)
        //        {
        //            RequestMicPermissions();
        //        }
        //        else
        //        {
        //            tcsPermissions.TrySetResult(true);
        //        }

        //    }

        //    return tcsPermissions.Task;
        //}

        //public void OnRequestPermissionResult(bool isGranted)
        //{
        //    tcsPermissions.TrySetResult(isGranted);
        //}

        //void RequestMicPermissions()
        //{
        //    if (ActivityCompat.ShouldShowRequestPermissionRationale(MainActivity.Instance, Manifest.Permission.RecordAudio))
        //    {
        //        Snackbar.Make(MainActivity.Instance.FindViewById(Android.Resource.Id.Content),
        //                "Microphone permissions are required for speech transcription!",
        //                Snackbar.LengthIndefinite)
        //                .SetAction("Ok", v =>
        //                {
        //                    ((Activity)MainActivity.Instance).RequestPermissions(permissions, RecordAudioPermissionCode);
        //                })
        //                .Show();
        //    }
        //    else
        //    {
        //        ActivityCompat.RequestPermissions((Activity)MainActivity.Instance, permissions, RecordAudioPermissionCode);
        //    }
        //}
    }
}