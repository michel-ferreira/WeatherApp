using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WeatherApp.ItemViewModels;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<CountryItemViewModel> _countries;
        private bool _isRunning;
        private string _search;
        private List<CountryResponse> _myCountries;
        private DelegateCommand _searchCommand;

        public CountryPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Countries List";
            LoadCountryAsync();
        }

        public DelegateCommand SearchCommand => _searchCommand ?? (_searchCommand = new DelegateCommand(ShowCountries));

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                ShowCountries();
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }


        private async void LoadCountryAsync()
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Check your Internet Connection", "Accept");
                });
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlAPICountry"].ToString();

            Response response = await _apiService.GetListAsync<CountryResponse>(url, "/v3.1", "/all");

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            _myCountries = (List<CountryResponse>)response.Result;
            ShowCountries();
        }

        private void ShowCountries()
        {
            if (string.IsNullOrEmpty(Search))
            {
                Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(p =>
                new CountryItemViewModel(_navigationService)
                {
                    Name = p.Name,
                    Image = p.Image,

                }).ToList());
            }
            else
            {
                Countries = new ObservableCollection<CountryItemViewModel>(
                    _myCountries.Select(p =>
                    new CountryItemViewModel(_navigationService)
                    {
                        Name = p.Name,
                        Image = p.Image
                    })
                    .Where(p => p.Name.CountryName.ToLower().Contains(Search.ToLower()))
                    .ToList());
            }
        }
    }
}
