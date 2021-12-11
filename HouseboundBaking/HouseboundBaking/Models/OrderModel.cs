using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseboundBaking.Models
{
    public class OrderModel
    {
        public OrderModel() { }

        [PrimaryKey, AutoIncrement]
        public int OrderModelId { get; set; }

        // [ForeignKey(typeof(UserModel))]
        // public int UserId { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; } //email address

        public string FullName { get; set; }

        public decimal TotalPrice { get; set; }

        ////todo get working then, add in FK for confirmed order
        ////[ForeignKey(typeof(ConfirmedOrder))]
        //public int OrderDetailsId { get; set; }

        public DateTime DateOrdered { get; set; }

        public string StripeRefCode { get; set; }

        //1 for user, 0 for guest
        public int IsUser { get; set; }
    }
}