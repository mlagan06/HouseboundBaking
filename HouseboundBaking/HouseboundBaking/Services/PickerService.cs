using HouseboundBaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HouseboundBaking.Services
{
    public class PickerService
    {
        public static ObservableCollection<QuantityOLD> GetQuantitiesForProductPage()
        {
            //todo do i need to start key as 0, instead of 1, as when adding detials of order to OrderDetails model we are using value instead of key, as 
            //its a string, didnt do yet as need more testing and working on creating orderDetails,
            //also if changeis made need to update in product page, and SC as after order is placed new Quantity() {Key=1, Value="0"}, is created for temp - maybe change to where.
            var quantities = new ObservableCollection<QuantityOLD>()
            {
                new QuantityOLD() {Key=1, Value="0"},
                new QuantityOLD() {Key=2, Value="1"},
                new QuantityOLD() {Key=3, Value="2"},
                new QuantityOLD() {Key=4, Value="3"},
                new QuantityOLD() {Key=5, Value="4"},
                new QuantityOLD() {Key=6, Value="5"},
                new QuantityOLD() {Key=7, Value="6"},
                new QuantityOLD() {Key=8, Value="7"},
                new QuantityOLD() {Key=9, Value="8"},
                new QuantityOLD() {Key=10, Value="9"},
                new QuantityOLD() {Key=11, Value="10"}
            };
            return quantities;
        }

        //public static ObservableCollection<Quantity> GetQuantitiesForShoppingcart()
        //{
        //    var quantities = new ObservableCollection<Quantity>()
        //    {
        //        new Quantity() {Key=1, Value="1"},
        //        new Quantity() {Key=2, Value="2"},
        //        new Quantity() {Key=3, Value="3"},
        //        new Quantity() {Key=4, Value="4"},
        //        new Quantity() {Key=5, Value="5"},
        //        new Quantity() {Key=6, Value="6"},
        //        new Quantity() {Key=7, Value="7"},
        //        new Quantity() {Key=8, Value="8"},
        //        new Quantity() {Key=9, Value="9"},
        //        new Quantity() {Key=10, Value="10"},
        //    };
        //    return quantities;
        //}
    }
}