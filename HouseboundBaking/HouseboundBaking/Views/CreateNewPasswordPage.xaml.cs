using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseboundBaking.Views
{
    //When user resets password, temp password will be set to their email,
    //when user logs on with temp password will bring them to this screen to create a new password
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewPasswordPage : ContentPage
    {
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }

        public CreateNewPasswordPage()
        {
            InitializeComponent();
        }

        async void ResetButton_Clicked(Object sender, EventArgs e)
        {
            bool passwordsMatch = true;
            passwordEntry1.TextColor = Color.Black;
            passwordEntry2.TextColor = Color.Black;

            var errorBuilder = new StringBuilder();

            //todo - sometimes null works sometimes ""
            if (passwordEntry1.Text == null)
            {
                errorBuilder.AppendLine(("New password field is required").ToString());
            }
            if (passwordEntry2.Text == null)
            {
                errorBuilder.AppendLine(("Confirm password field is required").ToString());
            }
            if (passwordEntry1.Text != passwordEntry2.Text)
            {
                errorBuilder.AppendLine(("New password field doesn't match confirm password field").ToString());
                passwordsMatch = false;
            }

            if (passwordsMatch)
            {
                if (!myPasswordValidation1.IsValid)
                {
                    foreach (var error in myPasswordValidation1.Errors)
                    {
                        if (error is string)
                        {
                            errorBuilder.AppendLine(((string)error).ToString());
                            passwordEntry1.TextColor = Color.Red;
                        }
                    }
                }

                if (!myPasswordValidation2.IsValid)
                {
                    foreach (var error in myPasswordValidation2.Errors)
                    {
                        if (error is string)
                        {
                            errorBuilder.AppendLine(((string)error).ToString());
                            passwordEntry2.TextColor = Color.Red;
                        }
                    }
                }
            }

            //No errors
            if (errorBuilder.ToString() != "")
            {
                // await DisplayAlert("Error", "Please complete form", "OK");
                await DisplayAlert("Error", errorBuilder.ToString(), "OK");
            }
            else
            {
                //todo
                //show password changed popup and load account page
                await DisplayAlert("Password Reset", "Password has been successfully reset", "Ok");

                //2 being my account page
                await RootPage.NavigateFromMenu(2);
            }

        }
    }
}