using Android.App;
using Android.Content;
using Android.Gms.Auth.Api.Phone;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HouseboundBaking.Droid;
using HouseboundBaking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(HashKey_Android))]
namespace HouseboundBaking.Droid
{
    public class HashKey_Android : IHashService
    {
        public string GenerateHashkey()
        {
            return new AppHashKeyHelper().GetAppHashKey(global::Android.App.Application.Context);
        }
        public void StartSMSRetriverReceiver()
        {
            SmsRetrieverClient _client = SmsRetriever.GetClient(global::Android.App.Application.Context);
            // Starts SmsRetriever, which waits for ONE matching SMS message until timeout
            // (5 minutes). The matching SMS message will be sent via a Broadcast Intent with
            // action SmsRetriever#SMS_RETRIEVED_ACTION.
            var task = _client.StartSmsRetriever();

            // You could also Listen for success/failure of StartSmsRetriever initiation
            //task.AddOnSuccessListener(new SuccessListener());
            //task.AddOnFailureListener(new FailureListener());
        }
    }
}