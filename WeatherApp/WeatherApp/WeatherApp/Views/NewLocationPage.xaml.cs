using System;
using System.Collections.Generic;
using System.ComponentModel;
using WeatherApp.Models;
using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    public partial class NewLocationPage : ContentPage
    {
        public Location Location { get; set; }

        public NewLocationPage()
        {
            InitializeComponent();
            BindingContext = new NewLocationViewModel();
        }
    }
}