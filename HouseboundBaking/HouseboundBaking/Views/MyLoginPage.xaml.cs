using HouseboundBaking.Data;
using HouseboundBaking.Models;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseboundBaking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyLoginPage : ContentPage
    {
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }

        public MyLoginPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (App.IsUserLoggedIn)
                {
                    //2 being my account
                    RootPage.NavigateFromMenu(2);
                }

                //bool to check if user has just registered or reset password, if on, populate fields with details,
                //else when page loads fields will be empty
                if (App.EmailEntryText != string.Empty)
                {
                    emailEntry.Text = App.EmailEntryText;
                }
                else
                {
                    emailEntry.Text = "";
                }

                passwordEntry.Text = "";

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }


        // Multi validation
        async void LoginButton_Clicked(Object sender, EventArgs e)
        {
            bool nameIsEmpty = false;
            bool emailIsEmpty = false;

            passwordEntry.TextColor = Color.Black;
            emailEntry.TextColor = Color.Black;

            //if (!myMultiValidation1.IsValid && !myMultiValidation2.IsValid)
            var errorBuilder = new StringBuilder();

            //If button is clicked before any data is entered
            if (emailEntry.Text == "")
            {
                errorBuilder.AppendLine(("Email field is required").ToString());
            }
            if (passwordEntry.Text == "")
            {
                errorBuilder.AppendLine(("Password field is required").ToString());
            }


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



            if (!myPasswordValidation.IsValid)
            {
                //  var errorBuilder = new StringBuilder();

                foreach (var error in myPasswordValidation.Errors)
                {
                    if (error is string)
                    {
                        errorBuilder.AppendLine(((string)error).ToString());
                        passwordEntry.TextColor = Color.Red;
                    }
                }


                // await DisplayAlert("Error", errorBuilder.ToString(), "OK");
            }




            //No errors
            if (errorBuilder.ToString() != "")
            {
                // await DisplayAlert("Error", "Please complete form", "OK");
                await DisplayAlert("Error", errorBuilder.ToString(), "OK");
            }
            else
            {
                //check db table if bool is set for resetPassword, if it is load reset password page, instead of login,
                //allowing user to reset password and continue
                string tempPassword = "123456"; //get tempPassword for db sqlLite
                if (passwordEntry.Text == tempPassword && emailEntry.Text == "mlagan06@gmail.com")
                {
                    //9 is reset password page
                    App.IsUserLoggedIn = true;
                    await RootPage.NavigateFromMenu(9);
                }

                if (passwordEntry.Text == "password" && emailEntry.Text == "mlagan06@gmail.com")
                {
                    //2 is my account
                    App.IsUserLoggedIn = true;
                    await RootPage.NavigateFromMenu(2);
                }

                //put in check to check users db for email and password equals
                //TEST
                UserDatabaseController UsersController = new UserDatabaseController();

                ObservableCollection<UserModel> AllExistingUsers = new ObservableCollection<UserModel>();

                List<UserModel> UsersList = UsersController.DoesUserExist(emailEntry.Text, passwordEntry.Text);
                if (UsersList != null)
                {
                    AllExistingUsers = new ObservableCollection<UserModel>(UsersList as List<UserModel>);
                    if (AllExistingUsers.Count == 1)
                    {
                        App.IsUserLoggedIn = true;
                        App.UserID = AllExistingUsers[0].UserId;

                        //if (App.globalShoppingCartOC.Count > 0) this should mean that
                        //Login Or NewUser has been clicked from CheckoutPage,
                        //so return user to checkoutpage -> 4
                        if (App.globalShoppingCartOC.Count > 0)
                        {
                            await RootPage.NavigateFromMenu(4);
                        }
                        else
                        {
                            //else just normal functionality and load MyAccountPage
                            await RootPage.NavigateFromMenu(2);
                        }
                    }
                }

                //if code reaches here, user does not exist.
                await DisplayAlert("Error", "Username or/and password is incorrect", "OK");
            }

        }

        protected override void OnDisappearing()
        {
            passwordEntry.Text = "";
            emailEntry.Text = "";
        }

        //void ForgotPassword_Clicked(object sender, EventArgs args)
        //{
        //    //Open
        //    var v = 1;
        //}

        //void CreateAccount_Clicked(object sender, EventArgs args)
        //{
        //    //Open
        //    var v = 1;
        //}
    }
}