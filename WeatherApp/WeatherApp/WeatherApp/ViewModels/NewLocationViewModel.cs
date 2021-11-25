using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using WeatherApp.Models;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class NewLocationViewModel : BaseViewModel
    {
        private string city;
        private string country;

        private Regex City_R = new Regex(@"^[A-Z]\w+");
        private Regex Country_R = new Regex(@"^[A-Z]{2,3}");

        public NewLocationViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return (!String.IsNullOrWhiteSpace(city)
                        && !String.IsNullOrWhiteSpace(country)
                        && City_R.IsMatch(city)
                        && Country_R.IsMatch(country))
                    || (!String.IsNullOrWhiteSpace(city)
                        && String.IsNullOrEmpty(country)
                        && City_R.IsMatch(city));
        }

        public string City
        {
            get => city;
            set => SetProperty(ref city, value);
        }

        public string Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Location newLocation = new Location()
            {
                Id = Guid.NewGuid().ToString(),
                City = City,
                Country = Country
            };

            await DataStore.AddLocationAsync(newLocation);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
