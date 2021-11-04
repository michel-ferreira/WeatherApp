﻿using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Models;
using WeatherApp.Views;

namespace WeatherApp.ItemViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand =>
            _selectMenuCommand ??
            (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));



        private async void SelectMenuAsync()
        {
            await _navigationService.NavigateAsync
                ($"/{nameof(WeatherAppMasterDetailPage)}/NavigationPage/{PageName}");
        }
    }
}
