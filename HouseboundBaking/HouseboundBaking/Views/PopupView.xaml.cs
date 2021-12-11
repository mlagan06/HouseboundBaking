using HouseboundBaking.Data;
using HouseboundBaking.Models;
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
    public partial class PopupView //: ContentView
    {
        public ObservableCollection<OrderDetailsModel> PreviousOrderDetailsForUserLV { get; set; }

        OrdersDatabaseController RecentOrdersController = new OrdersDatabaseController();

        public ObservableCollection<UserModel> AllExistingUsersLV { get; set; }
        UserDatabaseController UserController = new UserDatabaseController();

        public bool SLOrders { get; set; }
        public bool SLUsers { get; set; }

        public PopupView(int OrderId, int UserId)
        {
            try
            {
                InitializeComponent();


                //Show Orders PopUp
                if (OrderId != -1)
                {
                    SLOrders = true;
                    SLUsers = false;

                    List<OrderDetailsModel> RecentOrderDetailsList = RecentOrdersController.GetAllOrderDetailsByOrderId(OrderId);
                    PreviousOrderDetailsForUserLV = new ObservableCollection<OrderDetailsModel>(RecentOrderDetailsList as List<OrderDetailsModel>);

                    int startingHeightOfSL = 180;
                    int countOfOrderDetailRecords = PreviousOrderDetailsForUserLV.Count;
                    var tempHeightofStackSearchResults = countOfOrderDetailRecords * 40;

                    if (countOfOrderDetailRecords > 1)
                    {
                        StackSearchResultsOuter.HeightRequest = startingHeightOfSL + tempHeightofStackSearchResults;
                    }
                    else
                    {
                        StackSearchResultsOuter.HeightRequest = startingHeightOfSL;
                    }

                    UserInfoLabel.Text = "Email: " + PreviousOrderDetailsForUserLV[0].UserName + " | " + "Mobile: " + PreviousOrderDetailsForUserLV[0].UserMobileNumber;

                }
                else //Show Users PopUp
                {
                    if (UserId != -1)
                    {
                        var a = 1;

                        SLOrders = false;
                        SLUsers = true;

                        List<UserModel> UsersList = UserController.GetUserWithId(UserId);
                        if (UsersList != null)
                        {
                            AllExistingUsersLV = new ObservableCollection<UserModel>(UsersList as List<UserModel>);
                        }

                        StackSearchResultsOuter.HeightRequest = 180;
                    }
                }

                BindingContext = this;

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}