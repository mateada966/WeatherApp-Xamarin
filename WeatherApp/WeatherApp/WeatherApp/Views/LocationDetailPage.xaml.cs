using System;
using System.ComponentModel;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp.Views
{
    public partial class LocationDetailPage : ContentPage
    {
        LocationDetailViewModel _viewModel;

        public LocationDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new LocationDetailViewModel();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Delete", "Are you sure?\nYou want to delete this Location?", "Yes", "No"))
            {
                _viewModel.DeleteLocation();
            }
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            _viewModel.GetWeatherInfo();
        }
    }
}