using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models;
using WeatherApp.Views;

namespace WeatherApp.ItemViewModels
{
    public class CountryItemViewModel : CountryResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand =>
            _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));

        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"country",this}
            };

            await _navigationService.NavigateAsync(nameof(CountryPage), parameters);
        }
    }
}
