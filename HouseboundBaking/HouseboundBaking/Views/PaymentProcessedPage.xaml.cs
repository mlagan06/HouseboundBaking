using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseboundBaking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentProcessedPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public PaymentProcessedPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (App.OrderId != -1 && App.UserEmail != "")
                {
                    Message.Text = "Thank you for payment of: £" + App.priceToPay.ToString()
                        + "\n\nYour order number is: " + App.OrderId + "\n\nAn email receipt has been sent to: \n" + App.UserEmail + " \n\nThank You\nThe Bottle Shop";
                }
                else
                {
                    Exception ex = new Exception("PaymentProcessedPage -> Error -> OrderId AND/OR UserEmail should not be empty.");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }



    }
}