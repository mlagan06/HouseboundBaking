using HouseboundBaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HouseboundBaking.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // private decimal TotalForAllItems;
        //public ObservableCollection<ProductModel> ShoppingCartList = new ObservableCollection<ProductModel>();
        public ObservableCollection<QuantityOLD> List_Quantites { get; set; }

        private ObservableCollection<ProductModel> _shoppingCartList;
        public ObservableCollection<ProductModel> ShoppingCartList
        {
            get
            {
                return _shoppingCartList;
            }
            set
            {
                //  if (_shoppingCartList == value) return;
                _shoppingCartList = value;
                OnPropertyChanged(nameof(ShoppingCartList));


                // NotifyPropertyChanged();
            }
        }

        private string _subTotalForAllItems;
        public string SubTotalForAllItems
        {
            get
            {
                return _subTotalForAllItems;
            }
            set
            {
                _subTotalForAllItems = value;
                OnPropertyChanged(nameof(SubTotalForAllItems));
            }
        }

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

        private bool _checkoutBtnVisibility;
        public bool CheckoutBtnVisibility
        {
            get
            {
                return _checkoutBtnVisibility;
            }
            set
            {
                _checkoutBtnVisibility = value;
                OnPropertyChanged(nameof(CheckoutBtnVisibility));
            }
        }

        private bool _shoppingCartFull;
        public bool ShoppingCartFull
        {
            get
            {
                return _shoppingCartFull;
            }
            set
            {
                _shoppingCartFull = value;
                OnPropertyChanged(nameof(ShoppingCartFull));
            }
        }

        private bool _shoppingCartEmpty;
        public bool ShoppingCartEmpty
        {
            get
            {
                return _shoppingCartEmpty;
            }
            set
            {
                _shoppingCartEmpty = value;
                OnPropertyChanged(nameof(ShoppingCartEmpty));
            }
        }

        public ShoppingCartViewModel()
        {
            ShoppingCartList = new ObservableCollection<ProductModel>();
        }

        public void Initialize()
        {
            // TotalForAllItems = 0.00M;
            //   List_Quantites = PickerService.GetQuantitiesForProductPage();
            //  var subtotalList = new List<decimal>();

            //   var x2 = 2;
            //If user returns from checkoutpage back to SC,
            //ShoppingcartList.Clear will also empty App.globalShoppingCartOC,
            //This bool should stop that, otherwise list needs to be cleared 
            // if (App.ReturnFromCheckoutPage != null)
            //   {

            //var tempShoppingCartOC = new ObservableCollection<ProductModel>();
            //if (App.ReturnFromCheckoutPage)
            //{
            //    tempShoppingCartOC = App.globalShoppingCartOC;
            //    App.ReturnFromCheckoutPage = false;
            //}


            //OnPropertyChanged("ShoppingCartList");
            // ShoppingCartList.Clear();

            //REPLACED //ShoppingCartList.Clear(); WITH below, as returning from checkoutpage to SC, addss extra products
            //POSSIBLE BREAK ON CODE HERE AS APPARENTLY BREAKS THE BINDING, MAY NEED TO RE-LOOK AT LATER
            ShoppingCartList = new ObservableCollection<ProductModel>();

            // OnPropertyChanged("ShoppingCartList");

            //foreach (var v in ShoppingCartList)
            //{
            //    ShoppingCartList.Remove(v);
            //}
            //  }
            //  App.ReturnFromCheckoutPage = false;

            // }

            ////if(App.globalShoppingCartOC == null)
            ////{
            ////    var e = 1;
            ////}

            //    var x = 1;

            if (App.globalShoppingCartOC != null)
            {
                // var x3 = 3;

                int totalCountOfAllProducts = App.globalShoppingCartOC.Count;
                int tempCountOfAllProducts = 0;

                foreach (ProductModel Model in App.globalShoppingCartOC)
                {
                    if (Model.Quantity > 0)
                    {
                        var _quantity = Convert.ToDecimal(Model.Quantity);
                        if (_quantity > 0)
                        {
                            Model.SubTotalForItem = _quantity * Model.Price;
                            //  Quantity tempQuantity = Model.SelectedQuantity;
                            //  Model.ListQuantites = List_Quantites;
                            //  Model.SelectedQuantity = Model.ListQuantites.SingleOrDefault(p => p.Key == tempQuantity.Key && p.Value == tempQuantity.Value);
                            ShoppingCartList.Add(Model);



                            CheckoutBtnVisibility = true;
                            ShoppingCartFull = true;
                            ShoppingCartEmpty = false;

                            //No longer need below line to cal TotalForAllItems as SCQuantityAndSubtotalUpdated() is called from picker call back when above line, model is added to SC List, so below line will double value, adding again
                            //TotalForAllItems = TotalForAllItems + Model.SubTotalForItem;

                            ////Removes '0' for picker, forces user to swipe to delete, because of binding only needs removed once,
                            ////and removes from all Model.ListQuantites
                            //if (isFirst)
                            //{
                            //    Quantity temp_Quantity = new Quantity() { Key = 1, Value = "0" };
                            //    Model.ListQuantites.Remove(Model.ListQuantites.Where(i => i.Key == temp_Quantity.Key).Single());
                            //    isFirst = false;
                            //}
                        }
                        else
                        {
                            SubTotalForAllItems = "";
                            tempCountOfAllProducts++; //product quantity can be 0, or null, depending on previously selected, then 0 is picked
                        }
                    }
                    else
                    {
                        tempCountOfAllProducts++;
                    }
                    //  else if (Convert.ToDecimal(Model.SelectedQuantity.Value) == 0) // product can be 0, or null, depending on previously selected, then 0 is picked
                    //{
                    //    tempCountOfAllProducts++;
                    //}
                }

                if (totalCountOfAllProducts == tempCountOfAllProducts)
                {
                    //user has opened SC page, without selecting any items,
                    //shopping cart is empty
                    ShoppingCartFull = false;

                    ShowShoppingCartIsEmptyLabel();

                }
                // SubTotalForAllItems = TotalForAllItems.ToString();
            }
            NoItemsInShoppingCart = CalculateQuantityOfItemsInShoppingCart().ToString();
        }

        public void ClearSCList()
        {
            ShoppingCartList.Clear();
        }

        public int CalculateQuantityOfItemsInShoppingCart()
        {
            int quantityOfProducts = 0;

            if (ShoppingCartList != null)
            {
                foreach (var product in ShoppingCartList)
                {
                    if (product.Quantity > 0)
                    {
                        quantityOfProducts += product.Quantity;
                    }
                }
            }
            return quantityOfProducts;
        }

        public void RemoveProductFromSC(ProductModel productToRemove)
        {
            // ShoppingCartList.
            ShoppingCartList.Remove(productToRemove);

            // Quantity tempQuantity = productToRemove.SelectedQuantity;
            // decimal tempQuantityValue = Convert.ToDecimal(tempQuantity.Value);
            // decimal subtotalToRemove = productToRemove.Price * tempQuantityValue;
            // TotalForAllItems -= subtotalToRemove;
            // SubTotalForAllItems = TotalForAllItems.ToString();

            //NoItemsInShoppingCart = CalculateQuantityOfItemsInShoppingCart().ToString();
            UpdateSCQuantityAndSubTotal();
        }

        public void UpdateSCQuantityAndSubTotal()
        {
            decimal TotalForAllItems = 0.00M;

            if (ShoppingCartList != null)
            {
                bool hasSCBeenEmptiedFromSCPage = true;

                foreach (ProductModel Model in ShoppingCartList)
                {
                    var _quantity = Convert.ToDecimal(Model.Quantity);
                    if (_quantity > 0)
                    {
                        hasSCBeenEmptiedFromSCPage = false;
                        Model.SubTotalForItem = _quantity * Model.Price;
                        TotalForAllItems += Model.SubTotalForItem;
                    }
                }

                if (hasSCBeenEmptiedFromSCPage)
                {
                    // ShoppingCartList = new ObservableCollection<ProductModel>();
                    ShoppingCartList.Clear();
                    ShowShoppingCartIsEmptyLabel();
                }

                var subTotal = TotalForAllItems.ToString();
                if (subTotal != "0.00")
                {
                    SubTotalForAllItems = "£" + TotalForAllItems.ToString();
                }
                else
                {
                    //clear subtotal
                    SubTotalForAllItems = "";
                }
            }

            NoItemsInShoppingCart = CalculateQuantityOfItemsInShoppingCart().ToString();
        }

        protected void ShowShoppingCartIsEmptyLabel()
        {
            ShoppingCartEmpty = true;
            CheckoutBtnVisibility = false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
