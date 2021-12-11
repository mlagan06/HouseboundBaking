using HouseboundBaking.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.ViewModels.Converters
{
    public class ProductIdToProductNameConverter : IValueConverter
    {
        ProductsDatabaseController pdb = new ProductsDatabaseController();
        public object Convert(object productId, Type targetType, object parameter, CultureInfo culture)
        {
            //todo atm products are loaded from code, but when finished will be loaded from db. so for now just return temp value
            //        //eg
            //        //sql.getProductName(value) //value being productid


            string resultAfterConvert = pdb.GetProductNameForAdminSection(productId.ToString());

            return resultAfterConvert;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
