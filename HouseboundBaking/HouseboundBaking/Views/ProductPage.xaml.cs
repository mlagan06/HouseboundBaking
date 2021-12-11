using HouseboundBaking.Data;
using HouseboundBaking.Models;
using HouseboundBaking.Services;
using HouseboundBaking.ViewModels;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseboundBaking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        //Microphone
        //  SpeechRecognizer recognizer;
        //   IMicrophoneService micService;
        //  bool isTranscribing = false;

        public ProductPageViewModel productPage_ViewModal;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        int tempQuantity = -1;

        // int globalNumerOfProductsInSearch = 0;
        public ProductPage()
        {
            try
            {
                InitializeComponent();

                //   micService = DependencyService.Resolve<IMicrophoneService>();

              //   // SQLiteFunctionality sql = new SQLiteFunctionality();
              //   int tablesDeleted = sql.DeleteAllTables_ReturnCountTablesDeleted();

                productPage_ViewModal = new ProductPageViewModel();


                //  System.Environment.Exit(0);

                //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#0080FF");

                BindingContext = productPage_ViewModal;

                // NavigationPage.SetHasBackButton(this, false);

                //todo what is this? (below)
                NavigationPage page = App.Current.MainPage as NavigationPage;

                var navigationPage = Application.Current.MainPage as NavigationPage;
                if (navigationPage != null)
                    navigationPage.BarBackgroundColor = Color.Black;
            }
            catch (Exception ex)
            {
                //TODO ONLY USE FOR DUBGGING, remove popup when published? or leave in and say contact support?
                DisplayAlert("Error - Product", "Please contact support", "Ok");
                Crashes.TrackError(ex);
            }
        }
        //if ((NavigationPage)Application.Current.MainPage != null)
        //{
        //    ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
        //}
        //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.OrangeRed;

        //  this.NavigationBar.BarTintColor = UIColor.Yellow;
        // this.Navigation.NavigationStack.

        //SearchListView.RowHeight = 50;
        //SearchListView.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
        //{
        //    if (e.PropertyName == "ItemsSource")
        //    {
        //        try
        //        {
        //            if (SearchListView.ItemsSource != null)
        //            {
        //                int numberOfProductsFromSearchBox = 0;
        //                var x = GetProducts(out numberOfProductsFromSearchBox, SearchBar.Text);

        //                SearchListView.HeightRequest = numberOfProductsFromSearchBox * 50;
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //};


        protected override void OnDisappearing()
        {
            App.globalShoppingCartOC = productPage_ViewModal.WineList;
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                productPage_ViewModal.Initialize();

                //Internet access, will only display message if error in connection
                CheckNetworkAccess Network = new CheckNetworkAccess();
                string networkMessage = Network.IsConnected();
                if (networkMessage != string.Empty)
                {
                    DisplayAlert("Connectivity", networkMessage, "Ok");
                }

                TrackEvents();

                if (!String.IsNullOrWhiteSpace(SearchBar.Text))
                {
                    SearchBar.Text = string.Empty;
                }

                // throw new Exception("ahhhh its a crash");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        private void TrackEvents()
        {
            //uses APP Center SDK to track event

            // Device Model (SMG-950U, iPhone10,6)
            var device = DeviceInfo.Model;

            // Manufacturer (Samsung)
            var manufacturer = DeviceInfo.Manufacturer;

            // Device Name (Motz's iPhone)
            var deviceName = DeviceInfo.Name;

            // Operating System Version Number (7.0)
            var version = DeviceInfo.VersionString;

            // Platform (Android)
            var platform = DeviceInfo.Platform;

            // Idiom (Phone)
            var idiom = DeviceInfo.Idiom;

            // Device Type (Physical)
            var deviceType = DeviceInfo.DeviceType;

            //var devicePlatform_ios = DevicePlatform.iOS;
            //var devicePlatform_andriod = DevicePlatform.Android;
            //var devicePlatform_uwp = DevicePlatform.UWP;
            //var devicePlatform_unknown = DevicePlatform.Unknown;

            //var deviceIdiom_phone = DeviceIdiom.Phone;
            //var deviceIdiom_tablet = DeviceIdiom.Tablet;
            //var deviceIdiom_desktop = DeviceIdiom.Desktop;
            //var deviceIdiom_tv = DeviceIdiom.TV;
            //var deviceIdiom_watch = DeviceIdiom.Watch;
            //var deviceIdiom_unknown = DeviceIdiom.Unknown;

            Analytics.TrackEvent("Products Page Loaded", new Dictionary<string, string>
            {
                { "Device", device },
                { "Manufacturer", manufacturer},
                { "DeviceName", deviceName},
                { "Version", version},
                { "Platform", platform.ToString()},
                { "Idiom", idiom.ToString()},
                { "DeviceType", deviceType.ToString()},
            });
        }

        private async void ShoppingCartClicked(object sender, EventArgs e)
        {
            MenuPage tempMenu = new MenuPage();
            int IdOfMenuClicked = tempMenu.GetIdForNavigationMenu("Shopping Cart");
            await RootPage.NavigateFromMenu(IdOfMenuClicked);
        }

        public void QuantityChanged(object sender, EventArgs e)
        {
            // if (tempQuantity != -100)
            //   {
            //todo should I be using binding from vm to get quantity, selcted item? see View of SC page
            // var a = Quantity;
            var picker = (Picker)sender;
            int NewQuantity = picker.SelectedIndex;
            //  tempQuantity = picker.SelectedIndex;

            //     string tempQuantity = NewQuantity.ToString();
            //    picker.id;

            // int ProductId = 1;// (Picker)sender;

            var SelectedProduct = (ProductModel)picker.BindingContext;


            //var item = (MyListModel)picker.BindingContext;

            //productPage_ViewModal.UpdateProductQuantity(ProductId, Quantity);



            productPage_ViewModal.WineList.Where(x => x.ProductId == SelectedProduct.ProductId).FirstOrDefault().Quantity = NewQuantity;
            // productPage_ViewModal.WineList.Where(x => x.ProductId == SelectedProduct.ProductId).FirstOrDefault().Old_Quantity = int.Parse(tempQuantity);

            productPage_ViewModal.UpdateSCQuantity();

            //    tempQuantity = -100;
            // }
        }

        private void SearchBar_TextChanged(object sender1, TextChangedEventArgs e1)
        {
            if (!String.IsNullOrWhiteSpace(SearchBar.Text))
            {
                StackSearchResults.IsVisible = true;
                //numberOfProducts not needed on this call
                int numberOfProducts = 0;
                SearchListView.ItemsSource = GetProducts(out numberOfProducts, e1.NewTextValue);

                SearchListView.RowHeight = 50;
                SearchListView.HeightRequest = numberOfProducts * 50;
            }
            else
            {
                StackSearchResults.IsVisible = false;
            }

            //SearchListView.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            //{
            //    if (e.PropertyName == "ItemsSource")
            //    {
            //        try
            //        {
            //            if (SearchListView.ItemsSource != null)
            //            {
            //                int numberOfProductsFromSearchBox = 0;
            //                var x = GetProducts(out numberOfProductsFromSearchBox, SearchBar.Text);

            //                SearchListView.HeightRequest = numberOfProductsFromSearchBox * 50;
            //            }
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //};

            //  globalNumerOfProductsInSearch = numberOfProducts;



            //  (sender1 as ListView).ForceUpdateSize();

            //(sender1 as ViewCell).ForceUpdateSize();
            // int sizeOfSearchResultsLV = numberOfProducts * 20;
            //SearchListView.HeightRequest = sizeOfSearchResultsLV;

            //  ObservableCollection<ProductModel> SearchResults = SearchListView.ItemsSource as ObservableCollection<ProductModel>;
            //  if (SearchResults != null)
            //  {
            // SearchResults.CollectionChanged += (sender, e) =>
            //   {
            //var adjust = Device.OS != TargetPlatform.Android ? 1 : -numberOfProducts + 1;
            //SearchListView.HeightRequest = (numberOfProducts * 20) - adjust;

            //var a = 1;
            //   };
            //
            //    var result = dataGroups.Where(dg => dg.ProductName == mydetails.ProductName).LastOrDefault();
            //    if (result != null)
            //    {
            //        this.WineListView.SelectedItem = result;
            //        this.WineListView.ScrollTo(this.WineListView.SelectedItem, ScrollToPosition.MakeVisible, false);
            //    }
            //}

            //  var a = 1;
            //  StackSearchResults.HeightRequest = 100;

        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            (sender as ViewCell).ForceUpdateSize();
        }

        private IEnumerable<ProductModel> GetProducts(out int numberOfProducts, string searchText = null)
        {
            var allProducts = productPage_ViewModal.WineList;

            searchText = searchText.ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                numberOfProducts = 0;
                return null;
            }

            numberOfProducts = allProducts.Count;
            var products = allProducts.Where(p => p.BrandName.ToLower().Contains(searchText) || searchText.Contains(p.BrandName.ToLower()) || p.Grape.ToLower().Contains(searchText) || searchText.Contains(p.Grape.ToLower()));

            //var productsGrape = allProducts.Where(p => p.Grape.ToLower().Contains(searchText) || searchText.Contains(p.Grape.ToLower()));

            //TODO LOGIC NEEDED ADDED TO ALLOW FOR BRAND NAME && GRAPE, TEST AND WRITE LATER

            numberOfProducts = 0;

            //todo is there a tider way to do this instead of iterating through, as count doesnt work?
            foreach (var p in products)
            {
                numberOfProducts += 1;
            }

            ////foreach (var p in productsGrape)
            ////{
            ////    numberOfProducts += 1;
            ////}

            //products = allProducts;
            return products;
        }

        private void ItemTappedFromSearchBar(object sender, ItemTappedEventArgs e)
        {
            var mydetails = e.Item as ProductModel;

            //SearchBarTextBox.Text = mydetails.ProductName;
            //SearchBarLabel.Text = mydetails.ProductName;
            // SearchBarLabel2.Text = mydetails.ProductName;
            // Application.Current.MainPage = new NavigationPage(new MyListPageDetail(mydetails.ProductName, mydetails.Image, mydetails.Price.ToString()));
            //SearchBar searchBar = (SearchBar)sender;
            //searchResults.ItemsSource = DataService.GetSearchResults(searchBar.Text);
            //THIS ONE   SearchBar.Text = mydetails.ProductName;

            //TODO add in proper functinality to search .contains() to all lists, then if true then call adjacent function
            //at this stage we will go with wine
            //  WineClicked(null, null);
            OpenWineList();

            //var last = WineListView.ItemsSource.Cast<object>().LastOrDefault();
            //WineListView.ScrollTo(last, ScrollToPosition.MakeVisible, true);

            ObservableCollection<ProductModel> dataGroups = this.WineListView.ItemsSource as ObservableCollection<ProductModel>;
            if (dataGroups != null)
            {
                //TODO LOGIC AND TESTING NEEDED ADDED, FOR BRAND NAME AND GRAPE
                var result = dataGroups.Where(dg => dg.BrandName == mydetails.BrandName && dg.Grape == mydetails.Grape).LastOrDefault();
                if (result != null)
                {
                    this.WineListView.SelectedItem = result;
                    this.WineListView.ScrollTo(this.WineListView.SelectedItem, ScrollToPosition.MakeVisible, false);
                }
            }

            SearchListView.ItemsSource = null;
            StackSearchResults.IsVisible = false;
            //}
        }

        void OpenWineList()
        {
            WineListView.IsVisible = true;
        }


        //async void MicrophoneClicked(object sender, EventArgs e)
        //{
        //    //to do is something happening where have to click twice to get test 'mc ' to display in search bar
        //    //test this when published, as cant test with emulator - as on azure site, only one click was needed
        //    //may need to re look at login,
        //    //then add in image for when being used,
        //    //so is once for on, and twice for off?

        //    bool isMicEnabled = await micService.GetPermissionAsync();

        //    // EARLY OUT: make sure mic is accessible
        //    if (!isMicEnabled)
        //    {
        //        UpdateTranscription("Please grant access to the microphone!");
        //        return;
        //    }

        //    // initialize speech recognizer 
        //    if (recognizer == null)
        //    {
        //        var config = SpeechConfig.FromSubscription(Constants.CognitiveServicesApiKey, Constants.CognitiveServicesRegion);
        //        recognizer = new SpeechRecognizer(config);
        //        recognizer.Recognized += (obj, args) =>
        //        {
        //            UpdateTranscription(args.Result.Text);
        //        };
        //    }

        //    // if already transcribing, stop speech recognizer
        //    if (isTranscribing)
        //    {
        //        //test remove this when published
        //        // UpdateTranscription("nout");

        //        try
        //        {
        //            await recognizer.StopContinuousRecognitionAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            UpdateTranscription(ex.Message);
        //        }
        //        isTranscribing = false;
        //    }

        //    // if not transcribing, start speech recognizer
        //    else
        //    {
        //        Device.BeginInvokeOnMainThread(() =>
        //        {
        //            InsertDateTimeRecord();
        //        });
        //        try
        //        {
        //            await recognizer.StartContinuousRecognitionAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            UpdateTranscription(ex.Message);
        //        }
        //        isTranscribing = true;
        //    }
        //    UpdateDisplayState();
        //}

        //void UpdateTranscription(string newText)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        if (!string.IsNullOrWhiteSpace(newText))
        //        {
        //            // var test = 1;
        //            // transcribedText.Text += $"{newText}\n";
        //            SearchBar.Text += $"{newText}\n";
        //            //SearchBar_TextChanged(null, null);
        //            //SearchBar.Text = "Mc";
        //        }
        //    });
        //}

        //void InsertDateTimeRecord()
        //{
        //    //var msg = $"=================\n{DateTime.Now.ToString()}\n=================";
        //    //UpdateTranscription(msg);
        //}

        //void UpdateDisplayState()
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        //TODO change icon when talking to show mic is on...yeooo with indicator running
        //        //Microphone turned on
        //        if (isTranscribing)
        //        {
        //            // transcribeButton.Text = "Stop";
        //            //transcribeButton.BackgroundColor = Color.Red;
        //            //transcribingIndicator.IsRunning = true;
        //            productPage_ViewModal.MicrophoneImageDisplay();

        //        }
        //        else //mic turned off
        //        {
        //            productPage_ViewModal.MicrophoneImageDisplay();
        //            //transcribeButton.Text = "Transcribe";
        //            //transcribeButton.BackgroundColor = Color.Green;
        //            //transcribingIndicator.IsRunning = false;
        //        }
        //    });
        //}
    }
}

//The MIT License(MIT)

//Copyright(c) 2016 James Montemagno / Refractored LLC

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