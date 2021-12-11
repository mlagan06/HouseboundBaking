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
    public partial class AdminLoginPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        public AdminLoginPage()
        {
            InitializeComponent();
            passwordEntry.Text = "";
        }

        protected override void OnAppearing()
        {
            try
            {
                passwordEntry.Text = "";
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async void PasswordEntered_Clicked(object sender, EventArgs e)
        {
            if (passwordEntry.Text != "1234")
            {
                DisplayAlert("Admin", "Password Incorrect", "Ok");
                return;
            }

            //3 is AdminPage
            await RootPage.NavigateFromMenu(3);
        }
    }
}