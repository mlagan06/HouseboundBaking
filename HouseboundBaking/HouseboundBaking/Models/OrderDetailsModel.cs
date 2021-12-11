using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseboundBaking.Models
{
    //todo why internal instead of private?
    public class OrderDetailsModel
    {
        public OrderDetailsModel() { }

        [PrimaryKey, AutoIncrement]
        public int OrderDetailsId { get; set; }

        //todo going to need a unique id,
        //and ConfirmedOrderId, to store new record for each product on order

        // [ForeignKey(typeof(UserModel))]
        // public int UserId { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [ForeignKey(typeof(ProductModel))]
        public int ProductId { get; set; }

        [ForeignKey(typeof(OrderModel))]
        public int OrderModelId { get; set; }

        public int Quantity { get; set; }

        public decimal PriceOfItem { get; set; }

        public decimal SubtotalForThisItem { get; set; }

        public decimal TotalPriceForAllProductsInOrder { get; set; }

        public DateTime DateOrdered { get; set; }

        public string UserMobileNumber { get; set; }
    }
}