using HouseboundBaking.Models;
using HouseboundBaking.Services;
using HouseboundBaking.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseboundBaking
{
    public partial class App : Application
    {
        public static string NoOfItemsInShoppingCartGlobalVar { get; set; }
        public static string NewPageToLoad { get; set; }
        public static ObservableCollection<ProductModel> globalShoppingCartOC { get; set; }
        public static Dictionary<int, int> GlobalWinePickerUniquieIdDict = new Dictionary<int, int>();
        public static int QuantityPreviouslyDeleted { get; set; }
        public static string priceToPay { get; set; }
        //todo make all private
        public static bool ReturnFromCheckoutPage { get; set; }

        public static string EmailEntryText { get; set; }

        public static bool IsUserLoggedIn { get; set; }
        public static int UserID { get; set; }

        public static int OrderId { get; set; }
        public static string UserEmail { get; set; }

        public App()
        {
            try
            {
                Xamarin.Forms.Device.SetFlags(new[] { "SwipeView_Experimental" });

                InitializeComponent();

                UserID = -1;

                //MainPage = new Speech();
                MainPage = new MainPage();

                NavigationPage page = App.Current.MainPage as NavigationPage;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //todo implement for uwp
            //"uwp={Your UWP App secret here};" +
            try
            {
                AppCenter.Start("android=6dac8e86-7fb0-499c-8cf9-8fded6c2af51;" +
                            "ios=5982e406-89d9-4437-9e3f-b48ec8215539;",
                            typeof(Analytics), typeof(Crashes));
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
