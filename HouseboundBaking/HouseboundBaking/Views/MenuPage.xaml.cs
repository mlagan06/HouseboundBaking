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
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            try
            {
                InitializeComponent();

                //TODO add image along side menu items on screen
                menuItems = new List<HomeMenuItem>
                    {
                        new HomeMenuItem {Id = MenuItemType.Products, Title="Products", Icon =  "xamarin_logo.png", Name = "test", IsStartVisible=true },
                        new HomeMenuItem {Id = MenuItemType.ShoppingCart, Title="Shopping Cart", Icon =  "xamarin_logo.png", IsStartVisible=true},
                       // new HomeMenuItem {Id = MenuItemType.Browse, Title="Browse", Icon =  "xamarin_logo.png"},
                        new HomeMenuItem {Id = MenuItemType.AdminLogin, Title="Admin", Icon =  "xamarin_logo.png", IsStartVisible=true},//,
                        new HomeMenuItem {Id = MenuItemType.MyLogin, Title="My Account", Icon =  "xamarin_logo.png", Name = "test", IsStartVisible=true },
                      //  new HomeMenuItem {Id = MenuItemType.CreateNewPassword, Title="Create New Password", Icon =  "xamarin_logo.png", Name = "test", IsStartVisible=true },
                        //new HomeMenuItem {Id = MenuItemType.MyAccount, Title="Account", Icon =  "xamarin_logo.png", Name = "test", IsStartVisible=false },
                       // new HomeMenuItem {Id = MenuItemType.CheckoutPage, Title="Checkout", Icon =  "xamarin_logo.png", IsStartVisible=true},
                      //  new HomeMenuItem {Id = MenuItemType.PaymentProcessedPage, Title="PaymentProcessedPage", Icon =  "xamarin_logo.png", IsStartVisible=true},
                    };

                ListViewMenu.ItemsSource = menuItems;

                // ListViewMenu.remo

                //   ListViewMenu.SelectedItem = menuItems[0];
                //ListViewMenu.ItemSelected += async (sender, e) =>
                //{
                //    if (e.SelectedItem == null)
                //        return;

                //    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                //    await RootPage.NavigateFromMenu(id);
                //};
                ListViewMenu.ItemSelected += async (sender, e) =>
                {
                    if (e.SelectedItem == null)
                        return;

                    var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                    var MenuBtnClicked = (string)((HomeMenuItem)e.SelectedItem).Title;

                    await RootPage.NavigateFromMenu(id);

                    ListViewMenu.SelectedItem = null;

                };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

            // ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#0080FF");
        }

        public int GetIdForNavigationMenu(string title)
        {
            //THIS IS NOT ID 'MENU ITEM TYPE' FROM MENU ITEM LIST, IT IS THE ID OF RECORD INSIDE THE MENU ITEMS LISTVIEW
            //as 'id' in first record is 'Id = MenuItemType.Products', but the id we need is the id of where this record is
            //stored inside menuItems ListView so, products will be '0', shoppingCart '1' etc...
            int IdOfMenuItemsRecordInsideList = 0;
            int tempId = -1;
            foreach (var v in menuItems)
            {
                if (v.Title == title)
                {
                    tempId = IdOfMenuItemsRecordInsideList;
                    break;
                }

                IdOfMenuItemsRecordInsideList++;
            }

            return tempId;
        }

        //private async void ShoppingCartClicked(object sender, EventArgs e)
        //{
        //    //App.NewPageToLoad = "Shopping Cart";
        //    MenuPage tempMenu = new MenuPage();
        //    int IdOfMenuClicked = tempMenu.GetIdForNavigationMenu("Shopping Cart");

        //    //App.globalShoppingCartOC = productPage_ViewModal.WineList;
        //    await RootPage.NavigateFromMenu(IdOfMenuClicked);
        //}
    }
}