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
    public partial class MyAccountPage : ContentPage
    {
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }

        public MyAccountPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (!App.IsUserLoggedIn)
                {
                    //if not logged in return to login screen
                    RootPage.NavigateFromMenu(6);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }


        async void Logout_Clicked(Object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            App.UserID = -1;
            App.OrderId = -1;
            App.UserEmail = "";

            //6 being login screen
            await RootPage.NavigateFromMenu(6);
        }
    }
}