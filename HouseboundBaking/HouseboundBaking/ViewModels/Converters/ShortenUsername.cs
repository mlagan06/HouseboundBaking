using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.ViewModels.Converters
{
    public class ShortenUsername : IValueConverter
    {
        public object Convert(object username, Type targetType, object parameter, CultureInfo culture)
        {
            string longUsername = username.ToString();

            string shortUsername = string.Empty;// = longUsername.Substring(longUsername.LastIndexOf('@') + 1);

            int findAt = longUsername.IndexOf("@", StringComparison.Ordinal);

            if (findAt > 0)
            {
                shortUsername = longUsername.Substring(0, findAt);
            }

            //string resultAfterConvert = "12345";// pdb.GetProductNameForAdminSection(username.ToString());

            return shortUsername;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}

