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
    public partial class OrderAcknowledgementPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        public OrderAcknowledgementPage()
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
                    Message.Text = "Thank you for your order"
                        + "\n\nYour order number is: " + App.OrderId
                        + "\n\nPlease pay £" + App.priceToPay.ToString() + " upon collection"
                        + "\n\nAn order confirmation has been sent to: \n" + App.UserEmail + " \n\nThank You\nHouseboundBaking";
                }
                else
                {
                    Exception ex = new Exception("OrderAcknowledgementPage -> Error -> OrderId AND/OR UserEmail should not be empty.");
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
