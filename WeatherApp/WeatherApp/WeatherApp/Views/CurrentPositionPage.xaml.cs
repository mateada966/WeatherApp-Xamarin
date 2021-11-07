using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.Weather;
using WeatherApp.Services.WeatherAPI;
using WeatherApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentPositionPage : ContentPage
    {
        CurrentPositionViewModel _viewModel;

        public CurrentPositionPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CurrentPositionViewModel();
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            _viewModel.GetCurrentPosition();
        }
    }
}
