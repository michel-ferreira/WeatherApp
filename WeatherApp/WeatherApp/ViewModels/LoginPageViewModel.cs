using GalaSoft.MvvmLight.Command;
using Prism.Navigation;
using System.ComponentModel;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsRunning
        {
            get => this.isRunning;

            set
            {
                if (this.isRunning != value)
                {
                    this.isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
        }

        public bool IsEnabled
        {
            get => this.isEnabled;

            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        public LoginPageViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email....",
                    "Accept");

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password....",
                    "Accept");

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect....",
                    "Accept");

                return;
            }
            Application.Current.MainPage = new WeatherAppMasterDetailPage { Detail = new NavigationPage(new MainPage()) };
        }
    }
}