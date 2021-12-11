using HouseboundBaking.Data;
using HouseboundBaking.Models;
using HouseboundBaking.Services;
using HouseboundBaking.ViewModels;
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
    public partial class CheckoutPage : ContentPage
    {
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }
        bool onOffDummyData = false;
        bool isAgeLimitChecked = false;
        CheckoutPageViewModel CheckoutPageVM = new CheckoutPageViewModel();
        public CheckoutPage()
        {
            InitializeComponent();
            BindingContext = CheckoutPageVM;
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                ClearAllFields();

                //Internet access, will only display message if error in connection
                CheckNetworkAccess Network = new CheckNetworkAccess();
                string networkMessage = Network.IsConnected();
                if (networkMessage != string.Empty)
                {
                    DisplayAlert("Connectivity", networkMessage, "Ok");
                }

                if (App.globalShoppingCartOC != null)
                {
                    CheckoutPageVM.CalculateQuantityOfItemsInShoppingCartCheckOutPage(App.globalShoppingCartOC);
                }
                else
                {
                    throw new Exception("Shopping Cart list empty on checkout page.");
                }

                if (App.IsUserLoggedIn)
                {
                    if (App.UserID != -1)
                    {
                        PopulateFieldsWithUserDetails();
                    }
                    else
                    {
                        throw new Exception("CheckoutPage -> OnAppearing() -> Error UserIsLoggedIn but returning -1 for UserId");
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private async void PopulateFieldsWithUserDetails()
        {
            UserDatabaseController UserDB = new UserDatabaseController();
            List<UserModel> User = UserDB.GetUserWithId(App.UserID);
            if (User.Count == 1)
            {
                nameEntry.Text = User[0].FullName;
                emailEntry.Text = User[0].Email;
                //address1.Text = User[0].AddressLine1;
                //address2.Text = User[0].AddressLine2;
                //town_city.Text = User[0].City;
                //county.Text = User[0].County;
                //country.Text = User[0].Country;
                //postcode.Text = User[0].Postcode;
                mobileNumber.Text = User[0].MobileNumber;
            }
            else
            {
                throw new Exception("CheckoutPage -> PopulateFieldsWithUserDetails() -> Error retrieving User Details");
            }
        }

        private async void ShoppingCartClicked(object sender, EventArgs e)
        {
            App.ReturnFromCheckoutPage = true;
            MenuPage tempMenu = new MenuPage();
            int IdOfMenuClicked = tempMenu.GetIdForNavigationMenu("Shopping Cart");
            await RootPage.NavigateFromMenu(IdOfMenuClicked);
        }


        //Todo -> whitespace cuasing flag in name, relook at regex
        //TODO -> Add validation for card,
        //TODO -> tidy UI


        // Multi validation
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {

                bool nameIsEmpty = false;
                bool emailIsEmpty = false;
                //bool addressLine1IsEmpty = false;
                //bool addressLine2IsEmpty = false;
                //bool townCityIsEmpty = false;
                //bool countyIsEmpty = false;
                //bool countryIsEmpty = false;
                //bool postcodeIsEmpty = false;
                bool mobileNumberIsEmpty = false;
                //bool cardNumberIsEmpty = false;
                //bool expiryIsEmpty = false;
                //bool cvvIsEmpty = false;

                nameEntry.TextColor = Color.Black;
                emailEntry.TextColor = Color.Black;
                //address1.TextColor = Color.Black;
                //address2.TextColor = Color.Black;
                //town_city.TextColor = Color.Black;
                //county.TextColor = Color.Black;
                //country.TextColor = Color.Black;
                //postcode.TextColor = Color.Black;
                mobileNumber.TextColor = Color.Black;
                //cardNo.TextColor = Color.Black;
                //expiry.TextColor = Color.Black;
                //cvv.TextColor = Color.Black;

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
                //if (address1.Text == null)
                //{
                //    errorBuilder.AppendLine(("Address line 1 is required").ToString());
                //    addressLine1IsEmpty = true;
                //}
                //if (address2.Text == null)
                //{
                //    errorBuilder.AppendLine(("Address line 2 is required").ToString());
                //    addressLine2IsEmpty = true;
                //}
                //if (town_city.Text == null)
                //{
                //    errorBuilder.AppendLine(("Town/City is required").ToString());
                //    townCityIsEmpty = true;
                //}
                //if (county.Text == null)
                //{
                //    errorBuilder.AppendLine(("County is required").ToString());
                //    countyIsEmpty = true;
                //}
                //if (country.Text == null)
                //{
                //    errorBuilder.AppendLine(("Country is required").ToString());
                //    countryIsEmpty = true;
                //}
                //if (postcode.Text == null)
                //{
                //    errorBuilder.AppendLine(("Postcode is required").ToString());
                //    countryIsEmpty = true;
                //}
                if (mobileNumber.Text == null)
                {
                    errorBuilder.AppendLine(("Mobile number is required").ToString());
                    mobileNumberIsEmpty = true;
                }
                //if (cardNo.Text == null)
                //{
                //    errorBuilder.AppendLine(("Card Number is required").ToString());
                //    countryIsEmpty = true;
                //}
                //if (expiry.Text == null)
                //{
                //    errorBuilder.AppendLine(("Expiry Date is required in MM/YY format").ToString());
                //    countryIsEmpty = true;
                //}
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

                //if (!myfirstLineOfAddressValidation.IsValid)
                //{
                //    if (!addressLine1IsEmpty)
                //    {
                //        foreach (var error in myfirstLineOfAddressValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                address1.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

                //if (!mySecondLineOfAddressValidation.IsValid)
                //{
                //    if (!addressLine2IsEmpty)
                //    {
                //        foreach (var error in mySecondLineOfAddressValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                address2.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

                //if (!myTown_CityValidation.IsValid)
                //{
                //    if (!townCityIsEmpty)
                //    {
                //        foreach (var error in myTown_CityValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                town_city.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

                //if (!myCountyValidation.IsValid)
                //{
                //    if (!countyIsEmpty)
                //    {
                //        foreach (var error in myCountyValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                county.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

                //if (!myCountryValidation.IsValid)
                //{
                //    if (!countryIsEmpty)
                //    {
                //        foreach (var error in myCountryValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                country.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}


                //if (!myPostcodeValidation.IsValid)
                //{
                //    if (!postcodeIsEmpty)
                //    {
                //        foreach (var error in myPostcodeValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                postcode.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

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

                //if (!mycardNumberValidation.IsValid)
                //{
                //    if (!cardNumberIsEmpty)
                //    {
                //        foreach (var error in mycardNumberValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                cardNo.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

                //if (!myexpiryValidation.IsValid)
                //{
                //    if (!expiryIsEmpty)
                //    {
                //        //check expiry date given is not in past
                //        int month = int.Parse(expiry.Text.Substring(0, 2));
                //        int year = int.Parse(expiry.Text.Substring(2, 2));
                //        DateTime date = new DateTime(year, month, 1);

                //        if (date < DateTime.Now)
                //        {
                //            //date is in past, so expiry date has passed
                //            errorBuilder.AppendLine(("Expiry is out of date").ToString());
                //        }

                //        foreach (var error in myexpiryValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                expiry.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

                //if (!mycvvValidation.IsValid)
                //{
                //    if (!cvvIsEmpty)
                //    {
                //        foreach (var error in mycvvValidation.Errors)
                //        {
                //            if (error is string)
                //            {
                //                errorBuilder.AppendLine(((string)error).ToString());
                //                cardNo.TextColor = Color.Red;
                //            }
                //        }
                //    }
                //}

                //if (!isAgeLimitChecked)
                //{
                //    errorBuilder.AppendLine(((string)"Please confirm you are over 18").ToString());
                //}


                //No errors
                if (errorBuilder.ToString() != "")
                {
                    // await DisplayAlert("Error", "Please complete form", "OK");
                    await DisplayAlert("Error", errorBuilder.ToString(), "OK");
                }
                else
                {
                    //test used from ios app, as cant use stripe payments, so pass this and jump to paymentProcessed and send email
                    if (nameEntry.Text.ToLower() == "test")
                    {
                        //success TEST IOS 
                        DisplayAlert("TEST Payment Successful", "TEST Payment Successful", "Ok");
                        int OrderId = -1;
                        RecordDetailsOfOrder(out OrderId, "Test ios");
                        SendEmailReceipt(OrderId, "£ Test ios", emailEntry.Text);
                        // LoadPaymentProcessedPage(OrderId, emailEntry.Text);
                        LoadOrderAcknowledgementPage(OrderId, emailEntry.Text);
                        var S = App.globalShoppingCartOC;
                        App.globalShoppingCartOC.Clear();
                    }
                    else
                    {
                        PayViaStripeAsync();
                    }
                    //await DisplayAlert("Well Done Kid", "Passed", "OK");
                }
            }
            catch
            {
                var x = 1;
            }

        }


        //todo comapre against 
        //https://www.youtube.com/watch?v=_b8kNxoGW3k&ab_channel=XamarinGuy
        //todo send email from myself, instead of stripe
        //todo get api from server
        //todo add validation for address and email 
        //todo look at: https://www.c-sharpcorner.com/article/mobile-local-databases-in-xamarin-forms-using-sqlite/ to see if need PCLStorage Package
        //add in all needed fields, name, addresss, phone number, etc


        public void PayViaStripeAsync()
        {
            try
            {
                //        StripeConfiguration.ApiKey = Secrets.StripeSecretApiKey;
                //        //StripeConfiguration.ApiKey = Secrets.StripePublishableApiKey;

                //        string cardno = cardNo.Text;
                //        string expMonth = expiry.Text.Substring(0, 2);
                //        string expYear = expiry.Text.Substring(2, 2);
                //        string cardCvv = cvv.Text;

                //        //todo get api key from server side,
                //        //get dummy card details from stripe,
                //        //look at other examples what is 'CreditCardOptions' class
                //        //add in try catch...current time on video is 25.24 mins

                //        //NOTE TODO NOT LATEST VERSION OF STRIPE, HAD TO DOWNGRADE TO GET 'CREDITCARDOPTIONS' WHY WAS IT REMOVED
                //        //Step 1: create card option
                //        TokenCardOptions stripeOption = new TokenCardOptions();
                //        stripeOption.Number = cardno;

                //        long expire_Year;
                //        expire_Year = long.Parse(expYear);
                //        stripeOption.ExpYear = expire_Year;

                //        long expire_Month;
                //        expire_Month = long.Parse(expMonth);
                //        stripeOption.ExpMonth = expire_Month;

                //        stripeOption.Cvc = cardCvv;

                //        //Step 2: Assign card to token object
                //        TokenCreateOptions stripeCard = new TokenCreateOptions();
                //        stripeCard.Card = stripeOption;

                //        TokenService service = new TokenService();
                //        Token newToken = service.Create(stripeCard);

                //        //step 3: assign the token to the source
                //        var option = new SourceCreateOptions
                //        {
                //            Type = SourceType.Card,
                //            Currency = "GBP",
                //            Token = newToken.Id
                //        };

                //        var sourceService = new SourceService();
                //        Source source = sourceService.Create(option);

                //        //Step 4:
                //        CustomerCreateOptions customer = new CustomerCreateOptions
                //        {
                //            Name = nameEntry.Text,
                //            // Email = "mlagan06@gmail.com",
                //            Email = emailEntry.Text,
                //            Description = "Test Setup Stripe",
                //            Address = new AddressOptions { Line1 = address1.Text, Line2 = address2.Text, City = town_city.Text, State = county.Text, Country = country.Text, PostalCode = postcode.Text }
                //        };

                //        var customerService = new CustomerService();
                //        var cust = customerService.Create(customer);

                //        //to do needs tested
                //        //as this is global variavl could preform calculation to double check,
                //        //by iterating through global OC
                //        if (App.priceToPay == null)
                //        {
                //            throw new Exception("App.priceToPay == null from CheckoutPage");
                //        }

                string price = App.priceToPay;
                string priceToUse = price.Replace(".", "");
                long pricelong;
                pricelong = long.Parse(priceToUse);

                //        //Step 5
                //        var chargeoption = new ChargeCreateOptions
                //        {
                //            Amount = pricelong,
                //            Currency = "GBP",
                //            ReceiptEmail = emailEntry.Text,
                //            Customer = cust.Id,
                //            Source = source.Id,
                //        };

                //        //Step 6
                //        var chargeService = new ChargeService();
                // Charge charge = chargeService.Create(chargeoption);
                //        if (charge.Status == "succeeded")
                //        {
                //success
                DisplayAlert("Order Acknowledgement", "Order Placed", "Ok");
                int OrderId = -1;
                // RecordDetailsOfOrder(out OrderId, charge.Id);
                RecordDetailsOfOrder(out OrderId, "00000");
                SendEmailReceipt(OrderId, price, emailEntry.Text);
                LoadOrderAcknowledgementPage(OrderId, emailEntry.Text);
                //LoadPaymentProcessedPage(OrderId, emailEntry.Text);
                var S = App.globalShoppingCartOC;
                App.globalShoppingCartOC.Clear();
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
                Crashes.TrackError(ex);

            }
            //        }
            //        else
            //        {
            //            //failed
            //            DisplayAlert("Payment Unsuccessful", "Payment Unsuccessful - Please try again", "Ok");
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        string exception = ex.ToString();
            //    }
        }


        //private async void LoadPaymentProcessedPage(int OrderId, string userEmail)
        private async void LoadOrderAcknowledgementPage(int OrderId, string userEmail)
        {
            MenuPage tempMenu = new MenuPage();

            ClearAllFields();

            App.OrderId = OrderId;
            App.UserEmail = userEmail;

            //4 being id of CheckoutPage
            //5 being id of payment processed
            // await RootPage.NavigateFromMenu(5);

            //12 being id of OrderAcknowledgement
            await RootPage.NavigateFromMenu(12);
        }

        private void ClearAllFields()
        {
            nameEntry.Text = "";
            emailEntry.Text = "";
            //address1.Text = "";
            //address2.Text = "";
            //town_city.Text = "";
            //county.Text = "";
            //country.Text = "";
            //postcode.Text = "";
            mobileNumber.Text = "";
            //cardNo.Text = "";
            //expiry.Text = "";
            //cvv.Text = "";

            dummyDataCheckBox.IsChecked = false;
            //   ageLimitCheckBox.IsChecked = false;
        }

        private void SendEmailReceipt(int OrderId, string priceOfOrder, string userEmail)
        {
            //userEmail = "mlagan06@gmail.com";

            //string body = "Thank you for your recent order totalling to £" + priceOfOrder + "\nYour order number is: " + OrderId + " \n\nEnjoy your drink and please drink responsibly.\n\nThank You\nHouseboundBakingper365";
            ////await Xamarin.Essentials.Email.ComposeAsync("HouseboundBaking - Order " + OrderId, body, userEmail);

            //try
            //{
            //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //    SmtpServer.Port = 587;
            //    SmtpServer.Host = "smtp.gmail.com";
            //    SmtpServer.EnableSsl = true;
            //    SmtpServer.UseDefaultCredentials = false;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("HouseboundBakingemail@gmail.com", "NewPassword123!");

            //    MailMessage mail = new MailMessage();
            //    mail.From = new MailAddress("HouseboundBakingemail@gmail.com");
            //    mail.To.Add(userEmail);
            //    mail.Subject = "HouseboundBaking - Order " + OrderId;
            //    mail.Body = body;

            //    ServicePointManager.ServerCertificateValidationCallback = delegate (object senders, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslp)
            //    { return true; };

            //    //send user email as receipt
            //    SmtpServer.Send(mail);

            //    //send copy of receipt to home email
            //    MailMessage mail2 = new MailMessage();
            //    mail2.From = new MailAddress("HouseboundBakingemail@gmail.com");
            //    mail2.To.Add("HouseboundBakingemail@gmail.com");
            //    mail2.Subject = "Copy - Receipt - Order " + OrderId;
            //    mail2.Body = body + "\n\n (Above email has been sent to: " + userEmail + ")";

            //    SmtpServer.Send(mail2);
            //}
            //catch (Exception ex)
            //{
            //    //Catch Error
            //    //DisplayAlert("Faild", ex.Message, "OK");
            //    string error = ex.ToString();

            //    Crashes.TrackError(ex);
            //}

            try
            {
                EmailService Email = new EmailService();
                bool EmailSendSuccess = Email.SendEmailReceipt(OrderId, priceOfOrder, userEmail);

                if (!EmailSendSuccess)
                {
                    Email.SendErrorFoundEmail("CheckoutPage->SendEmailReceipt", OrderId, "mlagan06@gmail.com");
                }
            }
            catch (Exception ex)
            {
                //  var error = ex;
                Crashes.TrackError(ex);
            }

            //var test = true;
        }

        private void RecordDetailsOfOrder(out int orderModelId, string StripeRefCode)
        {
            OrdersDatabaseController _ordersDatabaseController = new OrdersDatabaseController();
            int isUser = IsUserOrIsGuest();
            orderModelId = _ordersDatabaseController.RecordNewOrder(Convert.ToDecimal(App.priceToPay), emailEntry.Text, StripeRefCode, isUser, nameEntry.Text);
            _ordersDatabaseController.RecordNewOrderDetais(App.globalShoppingCartOC, orderModelId, Convert.ToDecimal(App.priceToPay), emailEntry.Text, mobileNumber.Text);
            //todo need to add in checks to see if correct entry to table, so return true if entry, false, if not, then trigger try catch if false
        }

        private int IsUserOrIsGuest()
        {
            //0 for guest 1 for user
            int isUser = 0;

            if (App.IsUserLoggedIn && App.UserID != -1)
            {
                isUser = 1;
            }

            return isUser;
        }

        private void DummyData_Clicked(object sender, CheckedChangedEventArgs e)
        {
            if (!onOffDummyData)
            {
                nameEntry.Text = "Jim Bob";
                emailEntry.Text = "mlagan06@gmail.com";
                //address1.Text = "50 Long Street";
                //address2.Text = "Dungannon";
                //town_city.Text = "Belfast";
                //county.Text = "Tyrone";
                //country.Text = "Ireland";
                //postcode.Text = "BTjasjq";
                mobileNumber.Text = "07394856388";
                //cardNo.Text = "4242424242424242";
                //expiry.Text = "0425";
                //cvv.Text = "121";

                onOffDummyData = true;
            }
            else
            {
                nameEntry.Text = "";
                emailEntry.Text = "";
                //address1.Text = "";
                //address2.Text = "";
                //town_city.Text = "";
                //county.Text = "";
                //country.Text = "";
                //postcode.Text = "";
                mobileNumber.Text = "";
                //cardNo.Text = "";
                //expiry.Text = "";
                //cvv.Text = "";

                onOffDummyData = false;
            }
        }

        //private void isAgeLimitChanged(object sender, EventArgs e)
        //{
        //    if (isAgeLimitChecked)
        //    {
        //        isAgeLimitChecked = false;
        //    }
        //    else
        //    {
        //        isAgeLimitChecked = true;
        //    }
        //}

    }
}

//SPDX identifier
//MIT

//License text
// MIT License

//Copyright(c) _____

//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


//MICROSOFT SOFTWARE LICENSE TERMS
//MICROSOFT WINDOWS UI LIBRARY 
//----------------------------------------
//IF YOU LIVE IN (OR ARE A BUSINESS WITH YOUR PRINCIPAL PLACE OF BUSINESS IN) THE UNITED STATES, PLEASE READ THE “BINDING ARBITRATION AND CLASS ACTION WAIVER” SECTION BELOW. IT AFFECTS HOW DISPUTES ARE RESOLVED.
//----------------------------------------
//These license terms are an agreement between you and Microsoft Corporation (or one of its affiliates). They apply to the software named above and any Microsoft services or software updates (except to the extent such services or updates are accompanied by new or additional terms, in which case those different terms apply prospectively and do not alter your or Microsoft’s rights relating to pre-updated software or services). IF YOU COMPLY WITH THESE LICENSE TERMS, YOU HAVE THE RIGHTS BELOW.  BY USING THE SOFTWARE, YOU ACCEPT THESE TERMS.
//1. INSTALLATION AND USE RIGHTS.
//  a) General.You may install and use any number of copies of the software to develop and test your applications.
//  b) Third Party Software. The software may include third party applications that are licensed to you under this agreement or under their own terms. License terms, notices, and acknowledgements, if any, for the third party applications may be accessible online at https://aka.ms/thirdpartynotices or in an accompanying notices file. Even if such applications are governed by other agreements, the disclaimer, limitations on, and exclusions of damages below also apply to the extent allowed by applicable law.

//2.DISTRIBUTABLE CODE.You are permitted to distribute (i.e. make available for third parties) software in applications you develop, as described in this Section. 
//  a) Distribution Rights.You may copy, modify, and distribute object code; and
//   i.Third Party Distribution. You may permit distributors of your applications to copy and distribute any of this distributable code you elect to distribute with your applications.
// b) Distribution Requirements.For any code you distribute, you must:

//   i.add significant primary functionality to it in your applications;
//ii.require distributors and external end users to agree to terms that protect it and Microsoft at least as much as this agreement; and
//iii.indemnify, defend, and hold harmless Microsoft from any claims, including attorneys’ fees, related to the distribution or use of your applications, except to the extent that any claim is based solely on the unmodified distributable code.
//c) Distribution Restrictions.You may not:

//i.use Microsoft’s trademarks or trade dress in your application in any way that suggests your application comes from or is endorsed by Microsoft; or
//ii.modify or distribute the source code of any distributable code so that any part of it becomes subject to any license that requires that the distributable code, any other part of the software, or any of Microsoft’s other intellectual property be disclosed or distributed in source code form, or that others have the right to modify it.
//3. DATA COLLECTION. The software may collect information about you and your use of the software and send that to Microsoft. Microsoft may use this information to provide services and improve Microsoft’s products and services. Your opt-out rights, if any, are described in the product documentation.Some features in the software may enable collection of data from users of your applications that access or use the software. If you use these features to enable data collection in your applications, you must comply with applicable law, including getting any required user consent, and maintain a prominent privacy policy that accurately informs users about how you use, collect, and share their data. You can learn more about Microsoft’s data collection and use in the product documentation and the Microsoft Privacy Statement at https://go.microsoft.com/fwlink/?LinkId=521839. You agree to comply with all applicable provisions of the Microsoft Privacy Statement.
//4. SCOPE OF LICENSE.The software is licensed, not sold. Microsoft reserves all other rights.Unless applicable law gives you more rights despite this limitation, you will not (and have no right to):
//  a) work around any technical limitations in the software that only allow you to use it in certain ways;
//b) reverse engineer, decompile, or disassemble the software, or attempt to do so, except and only to the extent permitted by licensing terms governing the use of open-source components that may be included with the software;
//c) remove, minimize, block, or modify any notices of Microsoft or its suppliers in the software;
//d) use the software in any way that is against the law or to create or propagate malware; or
//e) share, publish, distribute, or lend the software (except for any distributable code, subject to the terms above), provide the software as a stand-alone hosted solution for others to use, or transfer the software or this agreement to any third party.
//5. EXPORT RESTRICTIONS. You must comply with all domestic and international export laws and regulations that apply to the software, which include restrictions on destinations, end users, and end use. For further information on export restrictions, visit https://aka.ms/exporting.
//6.SUPPORT SERVICES.Microsoft is not obligated under this agreement to provide any support services for the software. Any support provided is “as is”, “with all faults”, and without warranty of any kind.
//7. UPDATES. The software may periodically check for updates, and download and install them for you. You may obtain updates only from Microsoft or authorized sources. Microsoft may need to update your system to provide you with updates. You agree to receive these automatic updates without any additional notice. Updates may not include or support all existing software features, services, or peripheral devices.
//8. BINDING ARBITRATION AND CLASS ACTION WAIVER. This Section applies if you live in (or, if a business, your principal place of business is in) the United States.  If you and Microsoft have a dispute, you and Microsoft agree to try for 60 days to resolve it informally. If you and Microsoft can’t, you and Microsoft agree to binding individual arbitration before the American Arbitration Association under the Federal Arbitration Act (“FAA”), and not to sue in court in front of a judge or jury. Instead, a neutral arbitrator will decide. Class action lawsuits, class-wide arbitrations, private attorney -general actions, and any other proceeding where someone acts in a representative capacity are not allowed; nor is combining individual proceedings without the consent of all parties. The complete Arbitration Agreement contains more terms and is at https://aka.ms/arb-agreement-1. You and Microsoft agree to these terms.
//9.TERMINATION.Without prejudice to any other rights, Microsoft may terminate this agreement if you fail to comply with any of its terms or conditions. In such event, you must destroy all copies of the software and all of its component parts.
//10. ENTIRE AGREEMENT. This agreement, and any other terms Microsoft may provide for supplements, updates, or third-party applications, is the entire agreement for the software.
//11. APPLICABLE LAW AND PLACE TO RESOLVE DISPUTES. If you acquired the software in the United States or Canada, the laws of the state or province where you live (or, if a business, where your principal place of business is located) govern the interpretation of this agreement, claims for its breach, and all other claims (including consumer protection, unfair competition, and tort claims), regardless of conflict of laws principles, except that the FAA governs everything related to arbitration. If you acquired the software in any other country, its laws apply, except that the FAA governs everything related to arbitration. If U.S. federal jurisdiction exists, you and Microsoft consent to exclusive jurisdiction and venue in the federal court in King County, Washington for all disputes heard in court (excluding arbitration). If not, you and Microsoft consent to exclusive jurisdiction and venue in the Superior Court of King County, Washington for all disputes heard in court (excluding 
//12. CONSUMER RIGHTS; REGIONAL VARIATIONS.This agreement describes certain legal rights.You may have other rights, including consumer rights, under the laws of your state or country. Separate and apart from your relationship with Microsoft, you may also have rights with respect to the party from which you acquired the software. This agreement does not change those other rights if the laws of your state or country do not permit it to do so.For example, if you acquired the software in one of the below regions, or mandatory country law applies, then the following provisions apply to you:
//  a) Australia.You have statutory guarantees under the Australian Consumer Law and nothing in this agreement is intended to affect those rights.
//  b) Canada.If you acquired this software in Canada, you may stop receiving updates by turning off the automatic update feature, disconnecting your device from the Internet (if and when you re-connect to the Internet, however, the software will resume checking for and installing updates), or uninstalling the software. The product documentation, if any, may also specify how to turn off updates for your specific device or software.
//  c) Germany and Austria.
//    i. Warranty. The properly licensed software will perform substantially as described in any Microsoft materials that accompany the software. However, Microsoft gives no contractual guarantee in relation to the licensed software.
//    ii. Limitation of Liability. In case of intentional conduct, gross negligence, claims based on the Product Liability Act, as well as, in case of death or personal or physical injury, Microsoft is liable according to the statutory law.
//    Subject to the foregoing clause ii., Microsoft will only be liable for slight negligence if Microsoft is in breach of such material contractual obligations, the fulfillment of which facilitate the due performance of this agreement, the breach of which would endanger the purpose of this agreement and the compliance with which a party may constantly trust in (so-called "cardinal obligations"). In other cases of slight negligence, Microsoft will not be liable for slight negligence.
//13. DISCLAIMER OF WARRANTY. THE SOFTWARE IS LICENSED “AS IS.” YOU BEAR THE RISK OF USING IT. MICROSOFT GIVES NO EXPRESS WARRANTIES, GUARANTEES, OR CONDITIONS. TO THE EXTENT PERMITTED UNDER APPLICABLE LAWS, MICROSOFT EXCLUDES ALL IMPLIED WARRANTIES, INCLUDING MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, AND NON-INFRINGEMENT.
//14. LIMITATION ON AND EXCLUSION OF DAMAGES. IF YOU HAVE ANY BASIS FOR RECOVERING DAMAGES DESPITE THE PRECEDING DISCLAIMER OF WARRANTY, YOU CAN RECOVER FROM MICROSOFT AND ITS SUPPLIERS ONLY DIRECT DAMAGES UP TO U.S. $5.00. YOU CANNOT RECOVER ANY OTHER DAMAGES, INCLUDING CONSEQUENTIAL, LOST PROFITS, SPECIAL, INDIRECT, OR INCIDENTAL DAMAGES.
//This limitation applies to (a) anything related to the software, services, content (including code) on third party Internet sites, or third party applications; and(b) claims for breach of contract, warranty, guarantee, or condition; strict liability, negligence, or other tort; or any other claim; in each case to the extent permitted by applicable law.
//It also applies even if Microsoft knew or should have known about the possibility of the damages. The above limitation or exclusion may not apply to you because your state, province, or country may not allow the exclusion or limitation of incidental, consequential, or other damages.
//Please note: As this software is distributed in Canada, some of the clauses in this agreement are provided below in French.
//Remarque: Ce logiciel étant distribué au Canada, certaines des clauses dans ce contrat sont fournies ci-dessous en français.
//EXONÉRATION DE GARANTIE.Le logiciel visé par une licence est offert « tel quel ». Toute utilisation de ce logiciel est à votre seule risque et péril. Microsoft n’accorde aucune autre garantie expresse.Vous pouvez bénéficier de droits additionnels en vertu du droit local sur la protection des consommateurs, que ce contrat ne peut modifier. La ou elles sont permises par le droit locale, les garanties implicites de qualité marchande, d’adéquation à un usage particulier et d’absence de contrefaçon sont exclues.
//LIMITATION DES DOMMAGES-INTÉRÊTS ET EXCLUSION DE RESPONSABILITÉ POUR LES DOMMAGES. Vous pouvez obtenir de Microsoft et de ses fournisseurs une indemnisation en cas de dommages directs uniquement à hauteur de 5,00 $ US.Vous ne pouvez prétendre à aucune indemnisation pour les autres dommages, y compris les dommages spéciaux, indirects ou accessoires et pertes de bénéfices.
//Cette limitation concerne:
//- tout ce qui est relié au logiciel, aux services ou au contenu (y compris le code) figurant sur des sites Internet tiers ou dans des programmes tiers; et
//- les réclamations au titre de violation de contrat ou de garantie, ou au titre de responsabilité stricte, de négligence ou d’une autre faute dans la limite autorisée par la loi en vigueur.
//Elle s’applique également, même si Microsoft connaissait ou devrait connaître l’éventualité d’un tel dommage. Si votre pays n’autorise pas l’exclusion ou la limitation de responsabilité pour les dommages indirects, accessoires ou de quelque nature que ce soit, il se peut que la limitation ou l’exclusion ci-dessus ne s’appliquera pas à votre égard.
//EFFET JURIDIQUE. Le présent contrat décrit certains droits juridiques. Vous pourriez avoir d’autres droits prévus par les lois de votre pays. Le présent contrat ne modifie pas les droits que vous confèrent les lois de votre pays si celles-ci ne le permettent pas.

//Xamarin.Essentials
//The MIT License (MIT)
//Copyright(c) Microsoft Corporation
//All rights reserved.
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


//The MIT License (MIT)

//Copyright(c).NET Foundation Contributors

//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

//20160427

//## Xamarin.Essentials

//Thank you for installing Xamarin.Essentials, be sure to read through our full documentation at:
//https://aka.ms/xamarinessentials

//## Setup

//Ensure that you install Xamarin.Essentials into all of your projects.

//For Android projects there is a small amount of setup needed to handle permissions. Please follow our short guide at:
//https://aka.ms/essentials-getstarted

//## Release Notes

//See our full release notes for more information: https://aka.ms/essentials-releasenotes



//The MIT License (MIT)
//Copyright(c).NET Foundation Contributors
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//20160427



//The MIT License (MIT)
//Copyright(c).NET Foundation Contributors
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//20160427



//https://csspeechstorage.blob.core.windows.net/drop/license201809.html

//MICROSOFT SOFTWARE LICENSE TERMS

//MICROSOFT COGNITIVE SERVICES SPEECH SDK

//IF YOU LIVE IN (OR ARE A BUSINESS WITH YOUR PRINCIPAL PLACE OF BUSINESS IN) THE UNITED STATES, PLEASE READ THE “BINDING ARBITRATION AND CLASS ACTION WAIVER” SECTION BELOW. IT AFFECTS HOW DISPUTES ARE RESOLVED.

//These license terms are an agreement between you and Microsoft Corporation (or one of its affiliates). They apply to the software named above and any Microsoft services or software updates (except to the extent such services or updates are accompanied by new or additional terms, in which case those different terms apply prospectively and do not alter your or Microsoft’s rights relating to pre-updated software or services). IF YOU COMPLY WITH THESE LICENSE TERMS, YOU HAVE THE RIGHTS BELOW.  BY USING THE SOFTWARE, YOU ACCEPT THESE TERMS.

//1.    INSTALLATION AND USE RIGHTS.
//a)    General.You may install and use any number of copies of the software to develop and test your applications integration of API(s) used to access Cognitive Services Speech Services.
//b)    Third Party Software. The software may include third party applications that are licensed to you under this agreement or under their own terms. License terms, notices, and acknowledgements, if any, for the third party applications may be accessible online at http://aka.ms/thirdpartynotices or in an accompanying notices file. Even if such applications are governed by other agreements, the disclaimer, limitations on, and exclusions of damages below also apply to the extent allowed by applicable law.
//c)    Competitive Benchmarking.If you are a direct competitor, and you access or use the software for purposes of competitive benchmarking, analysis, or intelligence gathering, you waive as against Microsoft, its subsidiaries, and its affiliated companies (including prospectively) any competitive use, access, and benchmarking test restrictions in the terms governing your software to the extent your terms of use are, or purport to be, more restrictive than Microsoft’s terms. If you do not waive any such purported restrictions in the terms governing your software, you are not allowed to access or use this software, and will not do so.
//2.    DISTRIBUTABLE CODE. The software may contain code you are permitted to distribute (i.e.make available for third parties) in applications you develop, as described in this Section.
//a)    Distribution Rights.The code and test files described below are distributable if included with the software.
//i.REDIST.TXT Files. You may copy and distribute the object code form of code listed on the REDIST list in the software, if any; and

//ii.Third Party Distribution. You may permit distributors of your applications to copy and distribute any of this distributable code you elect to distribute with your applications.

//b)    Distribution Requirements.For any code you distribute, you must:
//i.add significant primary functionality to it in your applications;

//ii.require distributors and external end users to agree to terms that protect it and Microsoft at least as much as this agreement; and

//iii.indemnify, defend, and hold harmless Microsoft from any claims, including attorneys’ fees, related to the distribution or use of your applications, except to the extent that any claim is based solely on the unmodified distributable code.

//c)    Distribution Restrictions.You may not:
//i.use Microsoft’s trademarks or trade dress in your application in any way that suggests your application comes from or is endorsed by Microsoft; or

//ii.modify or distribute the source code of any distributable code so that any part of it becomes subject to any license that requires that the distributable code, any other part of the software, or any of Microsoft’s other intellectual property be disclosed or distributed in source code form, or that others have the right to modify it.

//3.    DATA COLLECTION. The software may collect information about you and your use of the software and send that to Microsoft. Microsoft may use this information to provide services and improve Microsoft’s products and services. Your opt-out rights, if any, are described in the product documentation.Some features in the software may enable collection of data from users of your applications that access or use the software. If you use these features to enable data collection in your applications, you must comply with applicable law, including getting any required user consent, and maintain a prominent privacy policy that accurately informs users about how you use, collect, and share their data. You can learn more about Microsoft’s data collection and use in the product documentation and the Microsoft Privacy Statement at https://go.microsoft.com/fwlink/?LinkId=512132. You agree to comply with all applicable provisions of the Microsoft Privacy Statement.
//4.    SCOPE OF LICENSE.The software is licensed, not sold. Microsoft reserves all other rights.Unless applicable law gives you more rights despite this limitation, you will not (and have no right to):
//a)    work around any technical limitations in the software that only allow you to use it in certain ways;
//b)    reverse engineer, decompile, or disassemble the software, or attempt to do so, except and only to the extent permitted by licensing terms governing the use of open-source components that may be included with the software;
//c)    remove, minimize, block, or modify any notices of Microsoft or its suppliers in the software;
//d)    use the software in any way that is against the law or to create or propagate malware; or
//e)    share, publish, distribute, or lend the software (except for any distributable code, subject to the terms above), provide the software as a stand-alone hosted solution for others to use, or transfer the software or this agreement to any third party.
//5.    EXPORT RESTRICTIONS. You must comply with all domestic and international export laws and regulations that apply to the software, which include restrictions on destinations, end users, and end use. For further information on export restrictions, visit http://aka.ms/exporting.
//6.SUPPORT SERVICES.Microsoft is not obligated under this agreement to provide any support services for the software. Any support provided is “as is”, “with all faults”, and without warranty of any kind.
//7.    UPDATES. The software may periodically check for updates, and download and install them for you. You may obtain updates only from Microsoft or authorized sources. Microsoft may need to update your system to provide you with updates. You agree to receive these automatic updates without any additional notice. Updates may not include or support all existing software features, services, or peripheral devices.
//8.    BINDING ARBITRATION AND CLASS ACTION WAIVER. This Section applies if you live in (or, if a business, your principal place of business is in) the United States.  If you and Microsoft have a dispute, you and Microsoft agree to try for 60 days to resolve it informally. If you and Microsoft can’t, you and Microsoft agree to binding individual arbitration before the American Arbitration Association under the Federal Arbitration Act (“FAA”), and not to sue in court in front of a judge or jury. Instead, a neutral arbitrator will decide. Class action lawsuits, class-wide arbitrations, private attorney -general actions, and any other proceeding where someone acts in a representative capacity are not allowed; nor is combining individual proceedings without the consent of all parties. The complete Arbitration Agreement contains more terms and is at http://aka.ms/arb-agreement-1. You and Microsoft agree to these terms.
//9.TERMINATION.Without prejudice to any other rights, Microsoft may terminate this agreement if you fail to comply with any of its terms or conditions. In such event, you must destroy all copies of the software and all of its component parts.
//10. ENTIRE AGREEMENT. This agreement, and any other terms Microsoft may provide for supplements, updates, or third-party applications, is the entire agreement for the software.
//11. APPLICABLE LAW AND PLACE TO RESOLVE DISPUTES. If you acquired the software in the United States or Canada, the laws of the state or province where you live (or, if a business, where your principal place of business is located) govern the interpretation of this agreement, claims for its breach, and all other claims (including consumer protection, unfair competition, and tort claims), regardless of conflict of laws principles, except that the FAA governs everything related to arbitration. If you acquired the software in any other country, its laws apply, except that the FAA governs everything related to arbitration. If U.S. federal jurisdiction exists, you and Microsoft consent to exclusive jurisdiction and venue in the federal court in King County, Washington for all disputes heard in court (excluding arbitration). If not, you and Microsoft consent to exclusive jurisdiction and venue in the Superior Court of King County, Washington for all disputes heard in court (excluding arbitration).
//12.CONSUMER RIGHTS; REGIONAL VARIATIONS.This agreement describes certain legal rights.You may have other rights, including consumer rights, under the laws of your state or country. Separate and apart from your relationship with Microsoft, you may also have rights with respect to the party from which you acquired the software. This agreement does not change those other rights if the laws of your state or country do not permit it to do so.For example, if you acquired the software in one of the below regions, or mandatory country law applies, then the following provisions apply to you:
//a)       Australia.You have statutory guarantees under the Australian Consumer Law and nothing in this agreement is intended to affect those rights.

//b)       Canada.If you acquired this software in Canada, you may stop receiving updates by turning off the automatic update feature, disconnecting your device from the Internet (if and when you re-connect to the Internet, however, the software will resume checking for and installing updates), or uninstalling the software. The product documentation, if any, may also specify how to turn off updates for your specific device or software.

//c)       Germany and Austria.

//i.    Warranty. The properly licensed software will perform substantially as described in any Microsoft materials that accompany the software. However, Microsoft gives no contractual guarantee in relation to the licensed software.

//ii.   Limitation of Liability. In case of intentional conduct, gross negligence, claims based on the Product Liability Act, as well as, in case of death or personal or physical injury, Microsoft is liable according to the statutory law.

//Subject to the foregoing clause ii., Microsoft will only be liable for slight negligence if Microsoft is in breach of such material contractual obligations, the fulfillment of which facilitate the due performance of this agreement, the breach of which would endanger the purpose of this agreement and the compliance with which a party may constantly trust in (so-called "cardinal obligations"). In other cases of slight negligence, Microsoft will not be liable for slight negligence.
//13. DISCLAIMER OF WARRANTY. THE SOFTWARE IS LICENSED “AS IS.” YOU BEAR THE RISK OF USING IT. MICROSOFT GIVES NO EXPRESS WARRANTIES, GUARANTEES, OR CONDITIONS. TO THE EXTENT PERMITTED UNDER APPLICABLE LAWS, MICROSOFT EXCLUDES ALL IMPLIED WARRANTIES, INCLUDING MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, AND NON-INFRINGEMENT.
//14. LIMITATION ON AND EXCLUSION OF DAMAGES. IF YOU HAVE ANY BASIS FOR RECOVERING DAMAGES DESPITE THE PRECEDING DISCLAIMER OF WARRANTY, YOU CAN RECOVER FROM MICROSOFT AND ITS SUPPLIERS ONLY DIRECT DAMAGES UP TO U.S. $5.00. YOU CANNOT RECOVER ANY OTHER DAMAGES, INCLUDING CONSEQUENTIAL, LOST PROFITS, SPECIAL, INDIRECT, OR INCIDENTAL DAMAGES.
//This limitation applies to (a) anything related to the software, services, content (including code) on third party Internet sites, or third party applications; and(b) claims for breach of contract, warranty, guarantee, or condition; strict liability, negligence, or other tort; or any other claim; in each case to the extent permitted by applicable law.

//It also applies even if Microsoft knew or should have known about the possibility of the damages. The above limitation or exclusion may not apply to you because your state, province, or country may not allow the exclusion or limitation of incidental, consequential, or other damages.



//Please note: As this software is distributed in Canada, some of the clauses in this agreement are provided below in French.

//Remarque: Ce logiciel étant distribué au Canada, certaines des clauses dans ce contrat sont fournies ci-dessous en français.

//EXONÉRATION DE GARANTIE.Le logiciel visé par une licence est offert « tel quel ». Toute utilisation de ce logiciel est à votre seule risque et péril. Microsoft n’accorde aucune autre garantie expresse.Vous pouvez bénéficier de droits additionnels en vertu du droit local sur la protection des consommateurs, que ce contrat ne peut modifier. La ou elles sont permises par le droit locale, les garanties implicites de qualité marchande, d’adéquation à un usage particulier et d’absence de contrefaçon sont exclues.

//LIMITATION DES DOMMAGES-INTÉRÊTS ET EXCLUSION DE RESPONSABILITÉ POUR LES DOMMAGES. Vous pouvez obtenir de Microsoft et de ses fournisseurs une indemnisation en cas de dommages directs uniquement à hauteur de 5,00 $ US.Vous ne pouvez prétendre à aucune indemnisation pour les autres dommages, y compris les dommages spéciaux, indirects ou accessoires et pertes de bénéfices.

//Cette limitation concerne:

//•    tout ce qui est relié au logiciel, aux services ou au contenu (y compris le code) figurant sur des sites Internet tiers ou dans des programmes tiers; et

//•    les réclamations au titre de violation de contrat ou de garantie, ou au titre de responsabilité stricte, de négligence ou d’une autre faute dans la limite autorisée par la loi en vigueur.

//Elle s’applique également, même si Microsoft connaissait ou devrait connaître l’éventualité d’un tel dommage. Si votre pays n’autorise pas l’exclusion ou la limitation de responsabilité pour les dommages indirects, accessoires ou de quelque nature que ce soit, il se peut que la limitation ou l’exclusion ci-dessus ne s’appliquera pas à votre égard.

//EFFET JURIDIQUE. Le présent contrat décrit certains droits juridiques. Vous pourriez avoir d’autres droits prévus par les lois de votre pays. Le présent contrat ne modifie pas les droits que vous confèrent les lois de votre pays si celles-ci ne le permettent pas.


//https://github.com/rotorgames/Rg.Plugins.Popup/wiki

//The plugin has been developing and improving since the beginning of 2016 and you can use it absolutely free because the plugin is distributed by MIT license



//Twilio
//MICROSOFT SOFTWARE LICENSE TERMS

//MICROSOFT .NET LIBRARY

//These license terms are an agreement between you and Microsoft Corporation (or based on where you live, one of its affiliates). They apply to the software named above. The terms also apply to any Microsoft services or updates for the software, except to the extent those have different terms.

//IF YOU COMPLY WITH THESE LICENSE TERMS, YOU HAVE THE RIGHTS BELOW.

//1.    INSTALLATION AND USE RIGHTS.
//You may install and use any number of copies of the software to develop and test your applications. 

//2.    THIRD PARTY COMPONENTS. The software may include third party components with separate legal notices or governed by other agreements, as may be described in the ThirdPartyNotices file(s) accompanying the software.
//3.    ADDITIONAL LICENSING REQUIREMENTS AND/OR USE RIGHTS.
//a.     DISTRIBUTABLE CODE.  The software is comprised of Distributable Code. “Distributable Code” is code that you are permitted to distribute in applications you develop if you comply with the terms below.
//i.      Right to Use and Distribute.
//·        You may copy and distribute the object code form of the software.

//·        Third Party Distribution. You may permit distributors of your applications to copy and distribute the Distributable Code as part of those applications.

//ii.     Distribution Requirements. For any Distributable Code you distribute, you must
//·        use the Distributable Code in your applications and not as a standalone distribution;

//·        require distributors and external end users to agree to terms that protect it at least as much as this agreement; and

//·        indemnify, defend, and hold harmless Microsoft from any claims, including attorneys’ fees, related to the distribution or use of your applications, except to the extent that any claim is based solely on the unmodified Distributable Code.

//iii.   Distribution Restrictions. You may not
//·        use Microsoft’s trademarks in your applications’ names or in a way that suggests your applications come from or are endorsed by Microsoft; or

//·        modify or distribute the source code of any Distributable Code so that any part of it becomes subject to an Excluded License. An “Excluded License” is one that requires, as a condition of use, modification or distribution of code, that (i) it be disclosed or distributed in source code form; or(ii) others have the right to modify it.

//4.    DATA.
//a.     Data Collection. The software may collect information about you and your use of the software, and send that to Microsoft. Microsoft may use this information to provide services and improve our products and services.  You may opt-out of many of these scenarios, but not all, as described in the software documentation.  There are also some features in the software that may enable you and Microsoft to collect data from users of your applications. If you use these features, you must comply with applicable law, including providing appropriate notices to users of your applications together with Microsoft’s privacy statement. Our privacy statement is located at https://go.microsoft.com/fwlink/?LinkID=824704. You can learn more about data collection and its use from the software documentation and our privacy statement. Your use of the software operates as your consent to these practices.
//b.Processing of Personal Data. To the extent Microsoft is a processor or subprocessor of personal data in connection with the software, Microsoft makes the commitments in the European Union General Data Protection Regulation Terms of the Online Services Terms to all customers effective May 25, 2018, at https://docs.microsoft.com/en-us/legal/gdpr.
//5.SCOPE OF LICENSE.The software is licensed, not sold. This agreement only gives you some rights to use the software.Microsoft reserves all other rights.Unless applicable law gives you more rights despite this limitation, you may use the software only as expressly permitted in this agreement.In doing so, you must comply with any technical limitations in the software that only allow you to use it in certain ways. You may not
//·        work around any technical limitations in the software;

//·        reverse engineer, decompile or disassemble the software, or otherwise attempt to derive the source code for the software, except and to the extent required by third party licensing terms governing use of certain open source components that may be included in the software;

//·        remove, minimize, block or modify any notices of Microsoft or its suppliers in the software;

//·        use the software in any way that is against the law; or

//·        share, publish, rent or lease the software, provide the software as a stand-alone offering for others to use, or transfer the software or this agreement to any third party.

//6.    EXPORT RESTRICTIONS. You must comply with all domestic and international export laws and regulations that apply to the software, which include restrictions on destinations, end users, and end use. For further information on export restrictions, visit www.microsoft.com/exporting.  
//7.    SUPPORT SERVICES. Because this software is “as is,” we may not provide support services for it.
//8.    ENTIRE AGREEMENT. This agreement, and the terms for supplements, updates, Internet-based services and support services that you use, are the entire agreement for the software and support services.
//9.    APPLICABLE LAW.  If you acquired the software in the United States, Washington law applies to interpretation of and claims for breach of this agreement, and the laws of the state where you live apply to all other claims. If you acquired the software in any other country, its laws apply.
//10. CONSUMER RIGHTS; REGIONAL VARIATIONS.This agreement describes certain legal rights.You may have other rights, including consumer rights, under the laws of your state or country. Separate and apart from your relationship with Microsoft, you may also have rights with respect to the party from which you acquired the software. This agreement does not change those other rights if the laws of your state or country do not permit it to do so.For example, if you acquired the software in one of the below regions, or mandatory country law applies, then the following provisions apply to you:
//a)    Australia.You have statutory guarantees under the Australian Consumer Law and nothing in this agreement is intended to affect those rights.
//b)    Canada.If you acquired this software in Canada, you may stop receiving updates by turning off the automatic update feature, disconnecting your device from the Internet (if and when you re-connect to the Internet, however, the software will resume checking for and installing updates), or uninstalling the software. The product documentation, if any, may also specify how to turn off updates for your specific device or software.
//c)    Germany and Austria.
//(i)        Warranty.The software will perform substantially as described in any Microsoft materials that accompany it. However, Microsoft gives no contractual guarantee in relation to the software.

//(ii)       Limitation of Liability. In case of intentional conduct, gross negligence, claims based on the Product Liability Act, as well as in case of death or personal or physical injury, Microsoft is liable according to the statutory law.

//Subject to the foregoing clause (ii), Microsoft will only be liable for slight negligence if Microsoft is in breach of such material contractual obligations, the fulfillment of which facilitate the due performance of this agreement, the breach of which would endanger the purpose of this agreement and the compliance with which a party may constantly trust in (so-called "cardinal obligations"). In other cases of slight negligence, Microsoft will not be liable for slight negligence
//11. DISCLAIMER OF WARRANTY. THE SOFTWARE IS LICENSED “AS-IS.” YOU BEAR THE RISK OF USING IT. MICROSOFT GIVES NO EXPRESS WARRANTIES, GUARANTEES OR CONDITIONS. TO THE EXTENT PERMITTED UNDER YOUR LOCAL LAWS, MICROSOFT EXCLUDES THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//12. LIMITATION ON AND EXCLUSION OF REMEDIES AND DAMAGES. YOU CAN RECOVER FROM MICROSOFT AND ITS SUPPLIERS ONLY DIRECT DAMAGES UP TO U.S. $5.00. YOU CANNOT RECOVER ANY OTHER DAMAGES, INCLUDING CONSEQUENTIAL, LOST PROFITS, SPECIAL, INDIRECT OR INCIDENTAL DAMAGES.
//This limitation applies to (a) anything related to the software, services, content (including code) on third party Internet sites, or third party applications; and(b) claims for breach of contract, breach of warranty, guarantee or condition, strict liability, negligence, or other tort to the extent permitted by applicable law.


//It also applies even if Microsoft knew or should have known about the possibility of the damages. The above limitation or exclusion may not apply to you because your state or country may not allow the exclusion or limitation of incidental, consequential or other damages.


//Xamarin is not responsible for, nor does it grant any licenses to, third - party packages.Some packages may require or install dependencies which are governed by additional licenses.

//This component depends on Google Play Services Client Library, which is subject to the terms of Android Software Development Kit License Agreement

//Xamarin Component for Google Play Services Client Library
//The MIT License(MIT)

//Copyright(c).NET Foundation Contributors

//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files(the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and / or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

//20160427







