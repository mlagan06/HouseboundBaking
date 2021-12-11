using HouseboundBaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.Data
{
    public class HyperlinkLabel : Label
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkLabel), null);
        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }
        public string Url
        {
            get
            {
                return (string)GetValue(UrlProperty);
            }
            set
            {
                SetValue(UrlProperty, value);
            }
        }

        //public void SetBools()
        //{
        //    //10 ->CreateNewUser clicked from checkoutPage
        //    //11 -> Login clicked from checkoutPage
        //    // so this bool if TRUE should reload checkoutPage when new account created OR user logged in,
        //    //populating checkout with user details
        //    if (Url != null)
        //    {
        //        if (Url.Contains("10") || Url.Contains("6"))
        //        {
        //            App.LoginPageOrNewUserPageClickedFromCheckout = true;
        //        }
        //    }
        //}

        public HyperlinkLabel()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Color.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                //Command = new Command(async () => await Launcher.OpenAsync(Url))
                Command = new Command(async () => await RootPage.NavigateFromMenu(Convert.ToInt32(Url)))
            });
        }
    }
}
