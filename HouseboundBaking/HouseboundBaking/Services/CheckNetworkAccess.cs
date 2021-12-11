using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace HouseboundBaking.Services
{
    public class CheckNetworkAccess
    {
        public CheckNetworkAccess()
        { }

        public string IsConnected()
        {
            string networkState = Connectivity.NetworkAccess.ToString();
            string networkMessage = string.Empty;
            if (networkState == "Unknown")
            {
                networkMessage = "The state of the internet connectivity is not known.";
            }
            else if (networkState == "None")
            {
                networkMessage = "No internet connectivity.";
            }
            else if (networkState == "Local")
            {
                networkMessage = "Local network access only.";
            }
            else if (networkState == "ConstrainedInternet")
            {
                networkMessage = "Limited internet access";
            }

            return networkMessage;
        }

    }
}
