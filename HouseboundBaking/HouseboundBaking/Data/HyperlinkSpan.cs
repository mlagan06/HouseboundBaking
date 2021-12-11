using HouseboundBaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HouseboundBaking.Data
{
    public class HyperlinkSpan : Span
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan), null);

        MainPage RootPage { get => Xamarin.Forms.Application.Current.MainPage as MainPage; }

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkSpan()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Color.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                // Command = new Command(async () => await Launcher.OpenAsync(Url))
                //Command = new Command(async () => await RootPage.NavigateFromMenu(7, -1, ""))
                Command = new Command(async () => await RootPage.NavigateFromMenu(Convert.ToInt32(Url)))
            });
        }
    }
}