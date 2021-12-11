using AVFoundation;
using Foundation;
using HouseboundBaking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace HouseboundBaking.iOS.Services
{
    public class iOSMicrophoneService : IMicrophoneService
    {
        //TaskCompletionSource<bool> tcsPermissions;

        //public Task<bool> GetPermissionAsync()
        //{
        //    tcsPermissions = new TaskCompletionSource<bool>();
        //    RequestMicPermission();
        //    return tcsPermissions.Task;
        //}

        //public void OnRequestPermissionResult(bool isGranted)
        //{
        //    tcsPermissions.TrySetResult(isGranted);
        //}

        //void RequestMicPermission()
        //{
        //    var session = AVAudioSession.SharedInstance();
        //    session.RequestRecordPermission((granted) =>
        //    {
        //        tcsPermissions.TrySetResult(granted);
        //    });
        //}
    }
}