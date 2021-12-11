using HouseboundBaking.Models;
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
    public partial class MainPage : MasterDetailPage
    {
        int idOfNewPage;
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            try
            {
                InitializeComponent();

                MasterBehavior = MasterBehavior.Popover;

                MenuPages.Add((int)MenuItemType.Products, (NavigationPage)Detail);

                // ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#0080FF");

                SetValue(NavigationPage.HasNavigationBarProperty, false);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {

                    case (int)MenuItemType.Products:
                        MenuPages.Add(id, new NavigationPage(new ProductPage()));
                        break;
                    case (int)MenuItemType.ShoppingCart:
                        MenuPages.Add(id, new NavigationPage(new ShoppingCartPage()));
                        break;
                    // case (int)MenuItemType.Browse:
                    //    MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                    //    break;
                    case (int)MenuItemType.Admin:
                        MenuPages.Add(id, new NavigationPage(new AdminPage()));
                        break;
                    case (int)MenuItemType.CheckoutPage:
                        MenuPages.Add(id, new NavigationPage(new CheckoutPage()));
                        break;
                    case (int)MenuItemType.PaymentProcessedPage:
                        MenuPages.Add(id, new NavigationPage(new PaymentProcessedPage()));
                        break;
                    case (int)MenuItemType.MyLogin:
                        MenuPages.Add(id, new NavigationPage(new MyLoginPage()));
                        break;
                    case (int)MenuItemType.MyAccount:
                        MenuPages.Add(id, new NavigationPage(new MyAccountPage()));
                        break;
                    case (int)MenuItemType.NewUser:
                        MenuPages.Add(id, new NavigationPage(new NewUserPage()));
                        break;
                    case (int)MenuItemType.ForgotPassword:
                        MenuPages.Add(id, new NavigationPage(new ForgotPasswordPage()));
                        break;
                    case (int)MenuItemType.CreateNewPassword:
                        MenuPages.Add(id, new NavigationPage(new CreateNewPasswordPage()));
                        break;
                    case (int)MenuItemType.CreateNewUser:
                        MenuPages.Add(id, new NavigationPage(new CreateNewUserPage()));
                        break;
                    case (int)MenuItemType.AdminLogin:
                        MenuPages.Add(id, new NavigationPage(new AdminLoginPage()));
                        break;
                    case (int)MenuItemType.OrderAcknowledgement:
                        MenuPages.Add(id, new NavigationPage(new OrderAcknowledgementPage()));
                        break;
                }

                idOfNewPage = id;
            }

            idOfNewPage = id;

            var newPage = MenuPages[id];

            if (newPage != null)// && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;

                //  ListViewMenu.SelectedItem = 0;
            }
        }
    }
}
