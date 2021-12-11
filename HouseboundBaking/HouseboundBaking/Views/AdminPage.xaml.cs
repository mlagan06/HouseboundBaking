using HouseboundBaking.Services;
using HouseboundBaking.ViewModels;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Services;
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
    public partial class AdminPage : ContentPage
    {
        //   public ObservableCollection<OrderModel> PreviousOrdersForUser { get; set; }
        //  public ObservableCollection<OrderDetailsModel> PreviousOrderDetailsForUser { get; set; }

        //  OrdersDatabaseController RecentOrdersController = new OrdersDatabaseController();

        public AdminPageViewModel AdminPage_ViewModel;

        //DONE DONE - get listview to display all correcrt info
        //DONE DONE add link beside each entry, so can show popUp showing orderDetails
        //DONE DONE - get popup to show details of order
        //DONE DONE - load details orders to see if can get it to load, (maybe from new page), then put inside grey box
        //DONE DONE - tidy ui, so columns match title spacing
        //DONE DONE - get grey box of orderDetails to show/hide,
        //DONE DONE- get grey box of orderDetails to show in correct position
        //DONE DONE - OrderDetailsPopUp showing correct data
        //DONE DONE - todo onAppearing not being called - resulting in last order placed not being loaded.
        //DONE DONE - onAppearing was being called but wasnt updating LV..needed to reassign itemSource
        //DONE DONE - add in password first,
        //DONE DONE - VM for this page & make isVisible properties use proper MVVM coding & make PreviousOrdersForUser loaded from MVVM (using InotifyProperty)

        //TODO - (later date) is stripe ref needed on display?
        //todo (later date) add search filter option

        public AdminPage()
        {
            try
            {
                InitializeComponent();
                AdminPage_ViewModel = new AdminPageViewModel();

                //AdminPage_ViewModel.AccessGrantedToAdmin();

                BindingContext = AdminPage_ViewModel;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        protected override void OnDisappearing()
        {
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

                AdminPage_ViewModel.Initialize();

                AdminPage_ViewModel.OpenOrdersTab();

                //List<OrderModel> RecentOrdersList = RecentOrdersController.GetAllPreviousOrders();
                //if (RecentOrdersList != null)
                //{
                //    PreviousOrdersForUser = new ObservableCollection<OrderModel>(RecentOrdersList as List<OrderModel>);
                //    OrdersListView.ItemsSource = PreviousOrdersForUser;
                //}

                //show enter PW to gain access SL, and hide others
                // passwordEntry.Text = string.Empty;
                //StackLayoutPasswordEntry.IsVisible = true;
                //StackLayoutNoOrdersToShow.IsVisible = false;
                //StackLayoutOrders.IsVisible = false;

                //  List<OrderDetailsModel> RecentOrderDetailsList = RecentOrdersController.GetAllOrderDetailsByOrderId(1);
                //   PreviousOrderDetailsForUser = new ObservableCollection<OrderDetailsModel>(RecentOrderDetailsList as List<OrderDetailsModel>);
                //}
                //else
                //{
                //  //  ContentViewNoOrdersExist.IsVisible = true;
                //}
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }

        }

        async void OrdersTabClicked(object sender, EventArgs e)
        {
            //todo set visibility of orders and users text, to only show when password accepted
            AdminPage_ViewModel.OpenOrdersTab();
        }
        async void UsersTabClicked(object sender, EventArgs e)
        {
            AdminPage_ViewModel.OpenUsersTab();
        }

        //private void PasswordEntered_Clicked(object sender, EventArgs e)
        //{
        //    if (passwordEntry.Text != "1234")
        //    {
        //        DisplayAlert("Admin", "Password Incorrect", "Ok");
        //        return;
        //    }

        //    AdminPage_ViewModel.AccessGrantedToAdmin();
        //}

        private void ShowOrderDetails_Clicked(object Sender, EventArgs e)
        {
            //for button used:https://stackoverflow.com/questions/40913439/xamarin-forms-button-command-binding-inside-a-listview
            //todo need to change to mvvm 
            //https://stackoverflow.com/questions/47889370/get-listview-from-button-command-xamarin-forms-mvvm
            Button button = (Button)Sender;
            string OrderId = button.CommandParameter.ToString();

            PopupNavigation.Instance.PushAsync(new PopupView(int.Parse(OrderId), -1));
        }


        private void ShowUserDetails_Clicked(object Sender, EventArgs e)
        {
            Button button = (Button)Sender;
            string UserId = button.CommandParameter.ToString();

            PopupNavigation.Instance.PushAsync(new PopupView(-1, int.Parse(UserId)));

            //var a = 1;
            //todo add logic to show extra details of user
        }

    }
}