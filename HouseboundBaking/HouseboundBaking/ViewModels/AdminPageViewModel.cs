using HouseboundBaking.Data;
using HouseboundBaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HouseboundBaking.ViewModels
{
    public class AdminPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        OrdersDatabaseController RecentOrdersController = new OrdersDatabaseController();
        UserDatabaseController UsersController = new UserDatabaseController();
        //public ObservableCollection<OrderModel> PreviousOrdersForUser { get; set; }
        public ObservableCollection<OrderDetailsModel> PreviousOrderDetailsForUser { get; set; }

        private ObservableCollection<OrderModel> _previousOrdersForUsers;
        public ObservableCollection<OrderModel> PreviousOrdersForUsers
        {
            get
            {
                return _previousOrdersForUsers;
            }
            set
            {
                _previousOrdersForUsers = value;
                OnPropertyChanged(nameof(PreviousOrdersForUsers));
            }
        }

        private ObservableCollection<UserModel> _allExistingUsers;
        public ObservableCollection<UserModel> AllExistingUsers
        {
            get
            {
                return _allExistingUsers;
            }
            set
            {
                _allExistingUsers = value;
                OnPropertyChanged(nameof(AllExistingUsers));
            }
        }

        //private bool _sLPasswordEntryVisibility;
        //public bool SLPasswordEntryVisibility
        //{
        //    get
        //    {
        //        return _sLPasswordEntryVisibility;
        //    }
        //    set
        //    {
        //        _sLPasswordEntryVisibility = value;
        //        OnPropertyChanged(nameof(SLPasswordEntryVisibility));
        //    }
        //}

        private bool _sLNoOrdersToShow;
        public bool SLNoOrdersToShow
        {
            get
            {
                return _sLNoOrdersToShow;
            }
            set
            {
                _sLNoOrdersToShow = value;
                OnPropertyChanged(nameof(SLNoOrdersToShow));
            }
        }

        private bool _sLNoUsersToShow;
        public bool SLNoUsersToShow
        {
            get
            {
                return _sLNoUsersToShow;
            }
            set
            {
                _sLNoUsersToShow = value;
                OnPropertyChanged(nameof(SLNoUsersToShow));
            }
        }

        private bool _sLOrders;
        public bool SLOrders
        {
            get
            {
                return _sLOrders;
            }
            set
            {
                _sLOrders = value;
                OnPropertyChanged(nameof(SLOrders));
            }
        }

        private bool _sLUsers;
        public bool SLUsers
        {
            get
            {
                return _sLUsers;
            }
            set
            {
                _sLUsers = value;
                OnPropertyChanged(nameof(SLUsers));
            }
        }


        ////0 for guest 1 for user
        //private bool _isUserOrIsGuest;
        //public bool IsUserOrIsGuest
        //{
        //    get
        //    {
        //        return _isUserOrIsGuest;
        //    }
        //    set
        //    {
        //        _isUserOrIsGuest = value;
        //        OnPropertyChanged(nameof(IsUserOrIsGuest));
        //    }
        //}

        public void Initialize()
        {
            SLNoOrdersToShow = false;
            SLNoUsersToShow = false;

            //List<OrderModel> RecentOrdersList = RecentOrdersController.GetAllPreviousOrders();
            //if (RecentOrdersList != null)
            //{
            //    PreviousOrdersForUsers = new ObservableCollection<OrderModel>(RecentOrdersList as List<OrderModel>);
            //    if (PreviousOrdersForUsers == null || PreviousOrdersForUsers.Count < 1)
            //    {
            //        SLNoOrdersToShow = true;
            //        SLOrders = false;
            //    }
            //}
            //else
            //{
            //    SLNoOrdersToShow = true;
            //    SLOrders = false;
            //}

            //List<UserModel> UsersList = UsersController.GetAllUsers();
            //if (UsersList != null)
            //{
            //    AllExistingUsers = new ObservableCollection<UserModel>(UsersList as List<UserModel>);

            //    if (AllExistingUsers == null || AllExistingUsers.Count < 1)
            //    {
            //        SLNoUsersToShow = true;
            //    }
            //}
            //else
            //{
            //    SLNoUsersToShow = true;
            //}

            //show enter PW to gain access SL, and hide others
            // passwordEntry.Text = string.Empty;
            // SLPasswordEntryVisibility = true;
            //   SLOrders = false;
            //InitialPageLoadRemoveTabs();
        }

        //public void AccessGrantedToAdmin()
        //{
        //    if (PreviousOrdersForUsers != null)
        //    {
        //        if (PreviousOrdersForUsers.Count > 0)
        //        {
        //            SLNoOrdersToShow = false;
        //           // SLPasswordEntryVisibility = false;
        //            SLOrders = true;
        //        }
        //        //else
        //        //{
        //        //    StackLayoutNoOrdersToShow.IsVisible = true;
        //        //    StackLayoutPasswordEntry.IsVisible = false;
        //        //    StackLayoutOrders.IsVisible = false;
        //        //}
        //    }
        //    else
        //    {
        //        SLNoOrdersToShow = true;
        //       // SLPasswordEntryVisibility = false;
        //        SLOrders = false;
        //    }

        //}
        public void OpenUsersTab()
        {
            SLNoOrdersToShow = false;
            SLOrders = false;
            SLUsers = true;

            List<UserModel> UsersList = UsersController.GetAllUsers();
            if (UsersList != null)
            {
                AllExistingUsers = new ObservableCollection<UserModel>(UsersList as List<UserModel>);

                if (AllExistingUsers == null || AllExistingUsers.Count < 1)
                {
                    SLNoUsersToShow = true;
                    SLUsers = false;
                }
            }
            else
            {
                SLNoUsersToShow = true;
                SLUsers = false;
            }
        }
        public void OpenOrdersTab()
        {
            SLOrders = true;
            SLNoUsersToShow = false;
            SLUsers = false;

            List<OrderModel> RecentOrdersList = RecentOrdersController.GetAllPreviousOrders();
            if (RecentOrdersList != null)
            {
                PreviousOrdersForUsers = new ObservableCollection<OrderModel>(RecentOrdersList as List<OrderModel>);
                if (PreviousOrdersForUsers == null || PreviousOrdersForUsers.Count < 1)
                {
                    SLNoOrdersToShow = true;
                    SLOrders = false;
                }
            }
            else
            {
                SLNoOrdersToShow = true;
                SLOrders = false;
            }

        }
        //public void InitialPageLoadRemoveTabs()
        //{
        //    SLOrders = false;
        //    SLUsers = false;
        //}
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
