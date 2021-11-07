using GalaSoft.MvvmLight.Command;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        private bool isEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public ICommand ChangeCommand
        {
            get
            {
                return new RelayCommand(Change);
            }
        }

        public MainPageViewModel()
        {
            this.IsEnabled = true;
        }

        private void Change()
        {
            Application.Current.MainPage = new WeatherAppMasterDetailPage { Detail = new NavigationPage(new MeteoPage()) };
        }
    }
}
