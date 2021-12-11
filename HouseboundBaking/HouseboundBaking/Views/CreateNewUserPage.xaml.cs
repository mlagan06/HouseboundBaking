using HouseboundBaking.Data;
using HouseboundBaking.Helpers;
using HouseboundBaking.Models;
using HouseboundBaking.Services;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace HouseboundBaking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewUserPage : ContentPage
    {
        // int otpNo = 0;

        bool onOffDummyData = false;
        bool GlobalBoolPhoneNumberIsVerified = false;
        // bool VerifyPhoneMessage = false;
        int GlobalRandomOTP = 0;
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }


        public CreateNewUserPage()
        {
            InitializeComponent();

            //wait for OTP
            MessagingCenter.Subscribe<string>(this, "ReceivedOTP", (message) =>
            {
                string[] words = message.Split();
                foreach (string item in words.ToList())
                {
                    //remove quotes from otp
                    string OTP_code = item.Substring(1, item.Length - 2);

                    var isNumeric = int.TryParse(OTP_code, out int otpCode);

                    if (isNumeric)
                    {
                        otp.Text = otpCode.ToString();
                        //otp.IsVisible = true;
                        // mobileNumber.IsVisible = false;
                        //otp.Text = item;
                        // btnNext.Text = "Login";
                        //DisplayAlert("Message", $"OTP is {item}", "Ok");
                        break;
                    }
                }
            });

            VerifiedPhoneMessage.IsVisible = false;
            GlobalBoolPhoneNumberIsVerified = false;
        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                VerifiedPhoneMessage.IsVisible = false;
                GlobalBoolPhoneNumberIsVerified = false;
                PhoneNumberVerifiedCheckBox.IsVisible = false;
                otp.IsVisible = true;
                otp.Text = "";
                VerifyOTPButton.IsVisible = true;
                VerifyNumberButton.IsVisible = true;
                ClearAllFields();

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async void ConfirmOTP_Clicked(object sender, EventArgs e)
        {
            //otp entered matches actual otp in text message
            if (otp.Text == GlobalRandomOTP.ToString())
            {
                //show success tick
                otp.IsVisible = false;
                VerifyOTPButton.IsVisible = false;
                GlobalBoolPhoneNumberIsVerified = true;

                VerifyNumberButton.IsVisible = false;

                // OTPstackLayout.IsVisible = true;


                VerifiedPhoneMessage.IsVisible = true;

                PhoneNumberVerifiedCheckBox.IsVisible = true;


            }
            else
            {
                await DisplayAlert("Error", $"Incorrect OTP", "Ok");
            }
        }

        //private async void SendOTP_Clicked2(object sender, EventArgs e)
        //{

        //    // send a GET request  
        //    string countryCode = "91";  // two digit only

        //    OTPModel model = new OTPModel();
        //    model.sender = "SOCKET";
        //    model.route = "4";
        //    model.country = countryCode;
        //    string hashKey = System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
        //    GenerateOTP();
        //    if (GlobalRandomOTP != 0)
        //    {
        //        string message = $"<#> Your OTP is {GlobalRandomOTP} {hashKey}";
        //        List<string> numbers = new List<string> { mobileNumber.Text };
        //        model.sms.Add(new Sms { message = message, to = numbers });

        //        var client = new RestClient($"https://api.msg91.com/api/v2/sendsms?country={countryCode}");
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("content-type", "application/json");
        //        request.AddHeader("authkey", "281817ABVlLRzJt5d0a30e2");
        //        string jsonData = JsonConvert.SerializeObject(model);
        //        request.AddParameter("application/json", jsonData, ParameterType.RequestBody);
        //        IRestResponse response = client.Execute(request);
        //        if (response.IsSuccessful)
        //        {
        //            DependencyService.Get<IHashService>().StartSMSRetriverReceiver();
        //            OTPResponse resp = JsonConvert.DeserializeObject<OTPResponse>(response.Content);
        //            if (resp.type == "success")
        //            {
        //                await DisplayAlert("Message", $"An OTP send to {numbers[0]}", "Ok");
        //            }
        //            else
        //            {
        //                // handle if sms failed to send
        //                await DisplayAlert("Message", resp.message, "Ok");
        //            }
        //        }
        //        else
        //        {
        //            DisplayAlert("Message", response.ErrorMessage, "Ok");
        //        }

        //        otp.IsVisible = true;
        //        VerifyOTPButton.IsVisible = true;
        //    }
        //}
        public void GenerateOTP()
        {
            //return new Random().Next(1000, 9999);
            Random RandomGen = new Random();
            GlobalRandomOTP = RandomGen.Next(1000, 9999);
            //GlobalRandomOTP = Convert.ToInt32(RandomOTP);
        }

        private async void SendOTP_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mobileNumber.Text))
            {
                await DisplayAlert("Error", "Please enter phone number first.", "OK");
                return;
            }

            if (mobileNumber.Text.Length < 11)
            {
                await DisplayAlert("Error", "Please enter 11 digit phone number.", "OK");
                return;
            }

            //if TRUE mobile exists
            if (CheckMobileNumberDoesNotExist())
            {
                await DisplayAlert("Error", "Mobile number already exists please choose another.", "OK");
                return;
            }

            string bodyTextMessage = "";
            string returnMessage = "";

            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            //   string accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            //   string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            string accountSid = Secrets.TwilioAccountid;
            string authToken = Secrets.TwilioAuthToken;

            TwilioClient.Init(accountSid, authToken);

            string mobile_Number = mobileNumber.Text.Remove(0, 1);
            mobile_Number = "+44" + mobile_Number;
            //TODO add code for country

            if (Device.RuntimePlatform == Device.Android)
            {
               // IHashService IHashSer = new IHashService();
                //string hashKey = IHashService.GenerateHashkey();
                string hashKey =  System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
               // string hashKey = "yfJxQEih0Bc";// System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
                    GenerateOTP();
                    if (GlobalRandomOTP != 0)
                    {
                        // bodyTextMessage = $"<#> Your OTP is '{GlobalRandomOTP}' Ref: {hashKey}";
                        bodyTextMessage = $"Your OTP is '{GlobalRandomOTP}' Ref: {hashKey}";
                    }

                    if (bodyTextMessage != "")
                    {
                        try
                        {
                            var message = MessageResource.Create(
                            body: bodyTextMessage,//"Your OTP is: " + GlobalRandomOTP,
                            from: new Twilio.Types.PhoneNumber("+19382536178"),
                            to: new Twilio.Types.PhoneNumber(mobile_Number));

                            returnMessage = message.Sid;
                        }
                        catch (Exception ex)
                        {
                            //string error = ex.Message;
                            await DisplayAlert("Message", "Mobile number does not exist, please try another.", "Ok");
                            return;
                        }
                    }

                    DependencyService.Get<IHashService>().StartSMSRetriverReceiver();
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                GenerateOTP();
                if (GlobalRandomOTP != 0)
                {
                    // bodyTextMessage = $"<#> Your OTP is '{GlobalRandomOTP}' Ref: {hashKey}";
                    bodyTextMessage = $"Your passcode OTP code is '{GlobalRandomOTP}'";
                }

                if (bodyTextMessage != "")
                {
                    var message = MessageResource.Create(
                    body: bodyTextMessage,//"Your OTP is: " + GlobalRandomOTP,
                    from: new Twilio.Types.PhoneNumber("+19382536178"),
                    to: new Twilio.Types.PhoneNumber(mobile_Number));

                    returnMessage = message.Sid;
                }
            }

            await DisplayAlert("Message", $"A 4 digit OTP is sent to {mobile_Number}. \nPlease enter OTP in text box and verify.", "Ok");

            otp.Focus();

            //   otp.IsVisible = true;
            //  VerifyOTPButton.IsVisible = true;
            // OTPstackLayout.IsVisible = true;

        }

        //implement permissions to read text message in ios/android

        //VerifyPhoneMessage.isVisible = true;

        //var e = "testc";

        //            ShowConfirmOTPPopUp();

        //SHOW POP UP
        //READ AND T AND C FROM CERTIFICATE
        //address1 IN SLECTION FROM MESSAGES, THAT SELECTS CODE
        // }

        //        private void GenerateOTP()
        //        {
        //            Random RandomGen = new Random();
        //            GlobalRandomOTP = RandomGen.Next(0, 9999).ToString("D4");
        //        }

        //        private async void SendOTP_Clicked(object sender, EventArgs e)
        //        {

        //            // send a GET request  
        //            //string countryCode = "91";  // two digit only

        //            //OTPModel model = new OTPModel();
        //            //model.sender = "SOCKET";
        //            //model.route = "4";
        //            //model.country = countryCode;
        //            string hashKey = System.Web.HttpUtility.UrlEncode(DependencyService.Get<IHashService>().GenerateHashkey());
        //            GenerateOTP();
        //            if (GlobalRandomOTP != "")
        //            {
        //                string bodyTextMessage = $"<#> Your OTP is {GlobalRandomOTP} {hashKey}";
        //               // List<string> numbers = new List<string> { mobileNumber.Text };
        //                OTPSendSMS(bodyTextMessage);
        //                //model.sms.Add(new Sms { message = message, to = numbers });
        //            }
        //            //var client = new RestClient($"https://api.msg91.com/api/v2/sendsms?country={countryCode}");
        //            //var request = new RestRequest(Method.POST);
        //            //request.AddHeader("content-type", "application/json");
        //            //request.AddHeader("authkey", "281817ABVlLRzJt5d0a30e2");
        //            //string jsonData = JsonConvert.SerializeObject(model);
        //            //request.AddParameter("application/json", jsonData, ParameterType.RequestBody);
        //            //IRestResponse response = client.Execute(request);
        //            //if (response.IsSuccessful)
        //            //{
        //            //    DependencyService.Get<IHashService>().StartSMSRetriverReceiver();
        //            //    OTPResponse resp = JsonConvert.DeserializeObject<OTPResponse>(response.Content);
        //            //    if (resp.type == "success")
        //            //    {
        //            //        await DisplayAlert("Message", $"An OTP send to {numbers[0]}", "Ok");
        //            //    }
        //            //    else
        //            //    {
        //            //        // handle if sms failed to send
        //            //        await DisplayAlert("Message", resp.message, "Ok");
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    DisplayAlert("Message", response.ErrorMessage, "Ok");
        //            //}
        //        }
        //        //public int GenerateOTP()
        //        //{
        //        //    return new Random().Next(1000, 9999);
        //        //}

        private async void RegisterButton_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (!GlobalBoolPhoneNumberIsVerified)
                {
                    await DisplayAlert("Error", "Please verify phone number before registering", "OK");
                    return;
                }

                bool nameIsEmpty = false;
                bool emailIsEmpty = false;
                bool addressLine1IsEmpty = false;
                bool addressLine2IsEmpty = false;
                bool townCityIsEmpty = false;
                bool countyIsEmpty = false;
                bool countryIsEmpty = false;
                bool postcodeIsEmpty = false;
                bool mobileNumberIsEmpty = false;
                bool passwordIsEmpty = false;

                nameEntry.TextColor = Color.Black;
                emailEntry.TextColor = Color.Black;
                address1.TextColor = Color.Black;
                address2.TextColor = Color.Black;
                town_city.TextColor = Color.Black;
                county.TextColor = Color.Black;
                country.TextColor = Color.Black;
                postcode.TextColor = Color.Black;
                mobileNumber.TextColor = Color.Black;
                password.TextColor = Color.Black;

                //if (!myMultiValidation1.IsValid && !myMultiValidation2.IsValid)
                var errorBuilder = new StringBuilder();

                //If button is clicked before any data is entered
                if (nameEntry.Text == null)
                {
                    errorBuilder.AppendLine(("Name field is required").ToString());
                }
                if (emailEntry.Text == null)
                {
                    errorBuilder.AppendLine(("Email field is required").ToString());
                }
                if (address1.Text == null)
                {
                    errorBuilder.AppendLine(("Address line 1 is required").ToString());
                    addressLine1IsEmpty = true;
                }
                if (address2.Text == null)
                {
                    errorBuilder.AppendLine(("Address line 2 is required").ToString());
                    addressLine2IsEmpty = true;
                }
                if (town_city.Text == null)
                {
                    errorBuilder.AppendLine(("Town/City is required").ToString());
                    townCityIsEmpty = true;
                }
                if (county.Text == null)
                {
                    errorBuilder.AppendLine(("County is required").ToString());
                    countyIsEmpty = true;
                }
                if (country.Text == null)
                {
                    errorBuilder.AppendLine(("Country is required").ToString());
                    countryIsEmpty = true;
                }
                if (postcode.Text == null)
                {
                    errorBuilder.AppendLine(("Postcode is required").ToString());
                    postcodeIsEmpty = true;
                }
                if (mobileNumber.Text == null)
                {
                    errorBuilder.AppendLine(("Mobile number is required").ToString());
                    mobileNumberIsEmpty = true;
                }
                if (password.Text == null)
                {
                    errorBuilder.AppendLine(("Password is required").ToString());
                    countryIsEmpty = true;
                }
                //if (cvv.Text == null)
                //{
                //    errorBuilder.AppendLine(("Expiry Date is required in MM/YY format").ToString());
                //    countryIsEmpty = true;
                //}





                if (!myFullNameValidation.IsValid)
                {
                    //  var errorBuilder = new StringBuilder();

                    foreach (var error in myFullNameValidation.Errors)
                    {
                        if (error is string)
                        {
                            errorBuilder.AppendLine(((string)error).ToString());
                            nameEntry.TextColor = Color.Red;
                        }
                    }


                    // await DisplayAlert("Error", errorBuilder.ToString(), "OK");
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

                if (!myfirstLineOfAddressValidation.IsValid)
                {
                    if (!addressLine1IsEmpty)
                    {
                        foreach (var error in myfirstLineOfAddressValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                address1.TextColor = Color.Red;
                            }
                        }
                    }
                }

                if (!mySecondLineOfAddressValidation.IsValid)
                {
                    if (!addressLine2IsEmpty)
                    {
                        foreach (var error in mySecondLineOfAddressValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                address2.TextColor = Color.Red;
                            }
                        }
                    }
                }

                if (!myTown_CityValidation.IsValid)
                {
                    if (!townCityIsEmpty)
                    {
                        foreach (var error in myTown_CityValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                town_city.TextColor = Color.Red;
                            }
                        }
                    }
                }

                if (!myCountyValidation.IsValid)
                {
                    if (!countyIsEmpty)
                    {
                        foreach (var error in myCountyValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                county.TextColor = Color.Red;
                            }
                        }
                    }
                }

                if (!myCountryValidation.IsValid)
                {
                    if (!countryIsEmpty)
                    {
                        foreach (var error in myCountryValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                country.TextColor = Color.Red;
                            }
                        }
                    }
                }


                if (!myPostcodeValidation.IsValid)
                {
                    if (!postcodeIsEmpty)
                    {
                        foreach (var error in myPostcodeValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                postcode.TextColor = Color.Red;
                            }
                        }
                    }
                }

                if (!myMobileNumberValidation.IsValid)
                {
                    if (!mobileNumberIsEmpty)
                    {
                        foreach (var error in myMobileNumberValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                mobileNumber.TextColor = Color.Red;
                            }
                        }
                    }
                }

                if (!myPasswordValidation.IsValid)
                {
                    if (!passwordIsEmpty)
                    {
                        foreach (var error in myPasswordValidation.Errors)
                        {
                            if (error is string)
                            {
                                errorBuilder.AppendLine(((string)error).ToString());
                                password.TextColor = Color.Red;
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
                    App.EmailEntryText = "";

                    //if TRUE user email already registered
                    if (CheckEmailAddressDoesNotExist())
                    {
                        // await DisplayAlert("Email", "Email address already taken please choose another.", "OK");
                        errorBuilder.AppendLine(("Email address already taken please choose another.").ToString());
                    }

                    //if TRUE user mobile number already registered
                    if (CheckMobileNumberDoesNotExist())
                    {
                        errorBuilder.AppendLine(("Phone number already taken please choose another.").ToString());
                    }

                    if (errorBuilder.ToString() != "")
                    {
                        // await DisplayAlert("Error", "Please complete form", "OK");
                        await DisplayAlert("Error", errorBuilder.ToString(), "OK");
                        return;
                    }

                    if (!CreateNewUser())
                    {
                        //fail safe, not actually needed
                        throw new Exception("CreateNewUser in UserModel DB fail");
                    }

                    App.EmailEntryText = emailEntry.Text;

                    //todo (here) add functionality for OTP to confirm mobile number
                    //for now, just load login page, with email already set

                    //6 being login page
                    await RootPage.NavigateFromMenu(6);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }
        private bool CheckEmailAddressDoesNotExist()
        {
            bool userFound = false;

            UserDatabaseController UserDB = new UserDatabaseController();
            List<UserModel> User = UserDB.GetUserByEmail(emailEntry.Text);
            if (User != null)
            {
                if (User.Count() > 0)
                {
                    userFound = true;
                }
            }

            return userFound;
        }

        private bool CheckMobileNumberDoesNotExist()
        {
            bool mobileFound = false;

            UserDatabaseController UserDB = new UserDatabaseController();
            List<UserModel> User = UserDB.GetUserByMobileNumber(mobileNumber.Text);
            if (User != null)
            {
                if (User.Count() > 0)
                {
                    mobileFound = true;
                }
            }

            return mobileFound;
        }

        private bool CreateNewUser()
        {
            bool userCreated = false;

            UserDatabaseController newUser = new UserDatabaseController();
            userCreated = newUser.CreateNewUser(nameEntry.Text, emailEntry.Text, address1.Text, address2.Text, town_city.Text, county.Text, country.Text, postcode.Text, mobileNumber.Text, password.Text);

            return userCreated;
        }

        private void ClearAllFields()
        {
            nameEntry.Text = "";
            emailEntry.Text = "";
            address1.Text = "";
            address2.Text = "";
            town_city.Text = "";
            county.Text = "";
            country.Text = "";
            postcode.Text = "";
            mobileNumber.Text = "";
            password.Text = "";

            dummyDataCheckBox.IsChecked = false;
        }


        private void DummyData_Clicked(object sender, CheckedChangedEventArgs e)
        {
            if (!onOffDummyData)
            {
                nameEntry.Text = "Jim Bob";
                emailEntry.Text = "mlagan06@gmail.com";
                address1.Text = "50 Long Street";
                address2.Text = "Dungannon";
                town_city.Text = "Belfast";
                county.Text = "Tyrone";
                country.Text = "Ireland";
                postcode.Text = "BTjasjq";
                mobileNumber.Text = "07394856388";
                password.Text = "password123";

                onOffDummyData = true;
            }
            else
            {
                nameEntry.Text = "";
                emailEntry.Text = "";
                address1.Text = "";
                address2.Text = "";
                town_city.Text = "";
                county.Text = "";
                country.Text = "";
                postcode.Text = "";
                mobileNumber.Text = "";
                password.Text = "";

                onOffDummyData = false;
            }
        }


    }
}