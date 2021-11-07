using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Browser.OpenAsync(new Uri("http://countryapp.somee.com/Account/Register"), BrowserLaunchMode.SystemPreferred);
        }
    }
}
