using HouseboundBaking.Models;
using HouseboundBaking.Services;
using HouseboundBaking.ViewModels;
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
    public partial class ShoppingCartPage : ContentPage
    {
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }
        ShoppingCartViewModel ShoppingCartViewModel = new ShoppingCartViewModel();
        ObservableCollection<ProductModel> TempSClist = new ObservableCollection<ProductModel>();
        // public static Dictionary<int, int> DictOfProductsAndOldQuantity = new Dictionary<int, int>();
        // public decimal TotalForAllItems;
        //This list will store quantity of products, so when '0' is selected to remove product, but then the user selects 'no' to remove,
        //picker will return to previous value by using value from this Tuple...yeoooooo
        List<Tuple<int, int>> myOldQuantityList = new List<Tuple<int, int>>();

        public ObservableCollection<QuantityOLD> List_Quantites { get; set; }

        public ShoppingCartPage()
        {
            InitializeComponent();
            BindingContext = ShoppingCartViewModel;
        }

        protected override void OnDisappearing()
        {
            App.globalShoppingCartOC = ShoppingCartViewModel.ShoppingCartList;
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                //Internet access, will only display message if error in connection
                CheckNetworkAccess Network = new CheckNetworkAccess();
                string networkMessage = Network.IsConnected();
                if (networkMessage != string.Empty)
                {
                    DisplayAlert("Connectivity", networkMessage, "Ok");
                }

                ShoppingCartViewModel.Initialize();

                var x = ShoppingCartViewModel.ShoppingCartList;

                myOldQuantityList.Clear();

                foreach (var v in ShoppingCartViewModel.ShoppingCartList)
                {
                    myOldQuantityList.Add(Tuple.Create(v.ProductId, v.Quantity));
                }

                var q = 1;


                //productPickerSC.SelectedIndex = 1;

                ////todo check if this works, and also populate differently as only laods once]
                //foreach(var v in ShoppingCartViewModel.ShoppingCartList)
                //{
                //    var tempProductId = v.ProductId;// - 1;
                //    DictOfProductsAndOldQuantity.Add(tempProductId, v.Quantity);
                //}
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            // StackLayoutShoppingCartFull.IsVisible = false;
            //            StackLayoutShoppingCartEmpty.IsVisible = true;
        }

        protected async void SI_Invoked(object sender, EventArgs e)
        {
            try
            {
                var si = sender as SwipeItem;
                var productToRemove = si.CommandParameter as ProductModel;

                var action = await DisplayAlert("Are you sure you want to remove ", productToRemove.BrandName + ", " + productToRemove.Grape + ".", "Yes", "No");

                if (action)
                {
                    ShoppingCartViewModel.RemoveProductFromSC(productToRemove);
                    //ShoppingCartViewModel.ShoppingCartList.CachingStrategy = RecycleElement;
                    //https://github.com/xamarin/Xamarin.Forms/issues/13790
                    //rollback to xamarin forms 4.8 until this is fixed - 8/ May/2021
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected async void SCQuantityAndSubtotalUpdated(object sender, EventArgs e)
        {
            try
            {
                //NOTE TODO this is temp fix, when xamarin fix swipeview delete, wil switch back to removing product from only swipe, and remove '0' from picker
                var picker = (Picker)sender;
                int NewQuantity = picker.SelectedIndex;
                var SelectedProduct = (ProductModel)picker.BindingContext;

                //  mylist.Add(SelectedProduct.ProductId, NewQuantity);

                if (SelectedProduct != null)
                {
                    if (NewQuantity == 0)
                    {
                        var action = await DisplayAlert("Are you sure you want to remove ", SelectedProduct.BrandName + ", " + SelectedProduct.Grape + ".", "Yes", "No");

                        if (action)
                        {
                            ShoppingCartViewModel.RemoveProductFromSC(SelectedProduct);
                            //ShoppingCartViewModel.ShoppingCartList.CachingStrategy = RecycleElement;
                            //https://github.com/xamarin/Xamarin.Forms/issues/13790
                            //rollback to xamarin forms 4.8 until this is fixed - 8/ May/2021
                        }
                        else
                        {
                            // int dictCount = DictOfProductsAndOldQuantity.Count;
                            //int value = DictOfProductsAndOldQuantity.Values.ElementAt(1);//ElementAt value should be <= _dict.Count - 1
                            // int key = DictOfProductsAndOldQuantity.Keys.ElementAt(1);//ElementAt value should be  < =_dict.Count - 1

                            var a = 1;

                            var x = myOldQuantityList;

                            var tempQuantity = myOldQuantityList.Where(p => p.Item1 == SelectedProduct.ProductId).FirstOrDefault().Item2;

                            var x1 = 1;

                            SelectedProduct.Quantity = tempQuantity;

                            //user has selected 'no' so return quantity to previous
                            // var oldQuantity = SelectedProduct.Old_Quantity;
                            //picker.SelectedIndex = oldQuantity;

                            var a1 = 1;
                            //SelectedProduct.Quantity = oldQuantity;
                        }
                    }

                    //myOldQuantityList is used to return picker to previous quantity, if user changes their mind when deleting a product,
                    //so they select '0' to remove, but then on the confirmation popUp they select, No they are not sure,
                    //so previous quantity will be restored in picker, by accessing it from the myOldQuantityList,
                    //as this list is populated on the OnAppearing, if quantity is updated/changed, before removal is attempted it will not have the new value,
                    //so 2 lines below are used to delete, old value, and add new one
                    myOldQuantityList.RemoveAll(w => w.Item1 == SelectedProduct.ProductId);
                    myOldQuantityList.Add(Tuple.Create(SelectedProduct.ProductId, SelectedProduct.Quantity));
                }

                ShoppingCartViewModel.UpdateSCQuantityAndSubTotal();
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }


        public void PlaceOrder_BtnClicked(object sender, EventArgs e)
        {
            LoadCheckout();
        }

        private async Task LoadCheckout()
        {
            //Removing £
            string price = ShoppingCartViewModel.SubTotalForAllItems.Remove(0, 1);
            App.priceToPay = price;

            //  MenuPage tempMenu = new MenuPage();
            //  int IdOfMenuClicked = tempMenu.GetIdForNavigationMenu("My Account");

            //todo load this figure from server side, so if code is refactored down the line,it wont break, change once, will change for all
            //4 being id of CheckoutPage
            await RootPage.NavigateFromMenu(4);
        }
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