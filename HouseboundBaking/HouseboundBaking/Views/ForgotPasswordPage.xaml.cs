using HouseboundBaking.Services;
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
    public partial class ForgotPasswordPage : ContentPage
    {
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }
        // bool ResetPasswordConfirmationLabel = false;
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                //if (App.IsUserLoggedIn)
                //{
                //    RootPage.NavigateFromMenu(2, -1, "");
                //}

                //bool to check if user has just registered, if on, populate fields with details,
                //else when page loads fields will be empty
                // passwordEntry.Text = "";
                emailEntry.Text = string.Empty;
                App.EmailEntryText = string.Empty;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        // Multi validation
        async void ResetButton_Clicked(Object sender, EventArgs e)
        {
            bool nameIsEmpty = false;
            bool emailIsEmpty = false;

            //passwordEntry.TextColor = Color.Black;
            emailEntry.TextColor = Color.Black;

            //if (!myMultiValidation1.IsValid && !myMultiValidation2.IsValid)
            var errorBuilder = new StringBuilder();

            //If button is clicked before any data is entered
            if (emailEntry.Text == "")
            {
                errorBuilder.AppendLine(("Email field is required").ToString());
            }
            //if (passwordEntry.Text == null)
            //{
            //    errorBuilder.AppendLine(("Password field is required").ToString());
            //}


            if (!myEmailValidation.IsValid)
            {
                // var errorBuilder = new StringBuilder();

                foreach (var error in myEmailValidation.Errors)
                {
                    if (error is string)
                    {
                        errorBuilder.AppendLine(((string)error).ToString());
                        emailEntry.TextColor = Color.Red;
                    }
                }
            }



            //if (!myPasswordValidation.IsValid)
            //{
            //    //  var errorBuilder = new StringBuilder();

            //    foreach (var error in myPasswordValidation.Errors)
            //    {
            //        if (error is string)
            //        {
            //            errorBuilder.AppendLine(((string)error).ToString());
            //            passwordEntry.TextColor = Color.Red;
            //        }
            //    }


            //    // await DisplayAlert("Error", errorBuilder.ToString(), "OK");
            //}




            //No errors
            if (errorBuilder.ToString() != "")
            {
                // await DisplayAlert("Error", "Please complete form", "OK");
                await DisplayAlert("Error", errorBuilder.ToString(), "OK");
            }
            else
            {
                ResetPasswordConfirmationLabel.IsVisible = true;

                SendEmailResetPassword(emailEntry.Text);

                //todo: write class to send email, then refactor sending email from checkout and also, send email from here:
                //send email with temp password, then use temp password to login, and load change password screen
                //showing new password and old

            }

        }

        async void LoginButton_Clicked(Object sender, EventArgs e)
        {
            // if (emailEntry.Text != string.Empty)
            // {
            App.EmailEntryText = emailEntry.Text;
            //6 being login page
            await RootPage.NavigateFromMenu(6);
            // }
        }


        private void SendEmailResetPassword(string userEmail)
        {
            EmailService Email = new EmailService();
            bool EmailSendSuccess = true;
            if (userEmail != null)
            {
                EmailSendSuccess = Email.SendResetPasswordEmail(userEmail);
            }

            if (!EmailSendSuccess)
            {
                Email.SendErrorFoundEmail("ForgotPasswordPage->SendEmailResetPassword", -1, userEmail);
            }
        }


    }
}