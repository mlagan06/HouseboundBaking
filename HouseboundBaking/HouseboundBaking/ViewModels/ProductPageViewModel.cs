using HouseboundBaking.Data;
using HouseboundBaking.Models;
using HouseboundBaking.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.ViewModels
{
    public class ProductPageViewModel : BindableObject, INotifyPropertyChanged
    {
        public ObservableCollection<ProductModel> WineList { get; set; }
        public ObservableCollection<QuantityOLD> List_Quantites { get; set; }

        public ProductsDatabaseController Products_DatabaseController = new ProductsDatabaseController();

        public bool MicrophoneTurnedOn = true;

        private string _noItemsInShoppingCart;
        public string NoItemsInShoppingCart
        {
            get
            {
                return _noItemsInShoppingCart;
            }
            set
            {
                _noItemsInShoppingCart = value;
                OnPropertyChanged(nameof(NoItemsInShoppingCart));
            }
        }


        private string _microphoneImage;
        public string MicrophoneImage
        {
            get
            {
                // _microphoneImage = "ShoppingCartImage.png";
                return _microphoneImage;
            }
            set
            {
                _microphoneImage = value;
                // _microphoneImage = "ShoppingCartImage.png";
                OnPropertyChanged(nameof(MicrophoneImage));
            }
        }


        public ProductPageViewModel()
        {
            List_Quantites = PickerService.GetQuantitiesForProductPage();

            var Wine_List = Products_DatabaseController.GetWineList();
            WineList = new ObservableCollection<ProductModel>(Wine_List as List<ProductModel>);


            var a = 1;

            //WineList.Add(new ProductModel { ProductId = 1, BrandName = "Mc guigans", Grape = "Red", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 2, BrandName = "Mc Guigans", Grape = "Chardonnay", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 3, BrandName = "Mc Guigans", Grape = "Merlot", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 4, BrandName = "Yellow Tail", Grape = "Shiraz", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 5, BrandName = "Yellow Tail", Grape = "Caber Merlot", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 5.50M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 6, BrandName = "Villa Marie", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 7, BrandName = "Mud House", Grape = "Sauvi Blanc", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 9, BrandName = "Blossom Hill", Grape = "Soft & Fruity", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 10, BrandName = "Blossom Hill", Grape = "Crisp & Dry", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 11, BrandName = "19 Crimes", Grape = "Red", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 20.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 12, BrandName = "19 Crimes", Grape = "Sauv Blanc", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 13, BrandName = "Blossom Hill", Grape = "Red", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 10.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 14, BrandName = "Mud House", Grape = "Red", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 20.00M, SubTotalForItem = 0.00M, Genre = "Wine" });
            //WineList.Add(new ProductModel { ProductId = 15, BrandName = "Oyster Bay", Grape = "Red", Image = "HomeBakerHat.png", Quantity =0, Description = "Fruity Flav", Size = "700ml", Price = 5.00M, SubTotalForItem = 0.00M, Genre = "Wine" });



        }

        public void Initialize()
        {
            if (App.globalShoppingCartOC != null)
            {
                ////First need to check shoppingCart list for product, if it has been removed in SC,
                ////then set quantity to 0
                foreach (var product in WineList)
                {
                    var doesProductExistInShoppingCart = App.globalShoppingCartOC.Where(x => x.ProductId == product.ProductId).FirstOrDefault();

                    if (doesProductExistInShoppingCart == null)
                    {
                        product.Quantity = 0;
                        // if (WineList.Where(x => x.ProductId == product.ProductId).FirstOrDefault().SelectedQuantity != null)
                        // {
                        // Quantity tempQuantity = new Quantity() { Key = 1, Value = "0" };
                        //product.ListQuantites = PickerService.GetQuantitiesForProductPage();
                        //  product.SelectedQuantity = List_Quantites.SingleOrDefault(p => p.Key == tempQuantity.Key && p.Value == tempQuantity.Value);

                        // }
                    }
                }

                foreach (ProductModel Model in App.globalShoppingCartOC)
                {
                    // if (Model.SelectedQuantity != null)
                    //{
                    //var _quantity = Convert.ToDecimal(Model.Quantity);
                    //if (_quantity > 0)
                    //{
                    //    Quantity tempQuantity = Model.SelectedQuantity;
                    //    Model.ListQuantites = PickerService.GetQuantitiesForProductPage();
                    //    Model.SelectedQuantity = Model.ListQuantites.SingleOrDefault(p => p.Key == tempQuantity.Key && p.Value == tempQuantity.Value);
                    //}
                    //  }
                }
            }

            MicrophoneTurnedOn = true;
            // MicrophoneImageDisplay();
            // MicrophoneImage = "MicrophoneOffImage.png";
            NoItemsInShoppingCart = CalculateQuantityOfItemsInShoppingCart().ToString();
        }

        public int CalculateQuantityOfItemsInShoppingCart()
        {
            int quantityOfProducts = 0;

            if (WineList != null)
            {
                foreach (var product in WineList)
                {
                    if (product.Quantity > 0)
                    {
                        quantityOfProducts += product.Quantity;
                    }
                }
            }

            return quantityOfProducts;
        }

        public void UpdateSCQuantity()
        {
            NoItemsInShoppingCart = CalculateQuantityOfItemsInShoppingCart().ToString();
        }

        //public void MicrophoneImageDisplay()
        //{
        //    if(MicrophoneTurnedOn)
        //    {
        //        MicrophoneTurnedOn = false;
        //        // MicrophoneImage = 
        //        MicrophoneImage = "MicrophoneOffImage.png";
        //    }
        //    else
        //    {
        //        MicrophoneTurnedOn = true;
        //        //MicrophoneImage = 
        //        MicrophoneImage = "MicrophoneOnImage.png";
        //    }
        //}

        //public void UpdateProductQuantity(int ProductId, int Quantity)
        //{
        //  //  Products_DatabaseController.UpdateQuantityOfProduct(ProductId, Quantity);
        //}
    }
}
