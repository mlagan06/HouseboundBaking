using HouseboundBaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.ViewModels
{
    public class CheckoutPageViewModel : BindableObject, INotifyPropertyChanged
    {
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

        public void CalculateQuantityOfItemsInShoppingCartCheckOutPage(ObservableCollection<ProductModel> GlobalShoppingCartOC)
        {
            int quantityOfProducts = 0;

            if (GlobalShoppingCartOC != null)
            {
                foreach (var product in GlobalShoppingCartOC)
                {
                    if (product.Quantity > 0)
                    {
                        quantityOfProducts += product.Quantity;
                    }
                }
            }
            //return quantityOfProducts;

            NoItemsInShoppingCart = quantityOfProducts.ToString();
        }

    }
}