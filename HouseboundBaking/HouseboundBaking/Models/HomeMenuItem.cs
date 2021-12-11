using System;
using System.Collections.Generic;
using System.Text;

namespace HouseboundBaking.Models
{
    public enum MenuItemType
    {
        Products, //0
        ShoppingCart, //1
        MyAccount, //2
        Admin, //3
        CheckoutPage, //4
        PaymentProcessedPage, //5
        MyLogin, //6
        NewUser, //7
        ForgotPassword, //8
        CreateNewPassword, //9
        CreateNewUser, //10
        AdminLogin,//11
        OrderAcknowledgement,//12
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public bool IsStartVisible { get; set; }
    }
}
