using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.ViewModels.Converters
{
    public class CheckIfUser : IValueConverter
    {
        public object Convert(object checkIfUser, Type targetType, object parameter, CultureInfo culture)
        {
            bool isUser = System.Convert.ToBoolean(checkIfUser);

            return isUser;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
