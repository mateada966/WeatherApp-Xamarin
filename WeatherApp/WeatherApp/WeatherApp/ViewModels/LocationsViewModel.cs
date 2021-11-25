using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Views;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class LocationsViewModel : BaseViewModel
    {
        private Location _selectedLocation;

        public ObservableCollection<Location> Locations { get; }
        public Command LoadLocationsCommand { get; }
        public Command AddLocationCommand { get; }
        public Command<Location> LocationTapped { get; }

        public LocationsViewModel()
        {
            Title = "Locations";
            Locations = new ObservableCollection<Location>();
            LoadLocationsCommand = new Command(async () => await ExecuteLoadLocationsCommand());

            LocationTapped = new Command<Location>(OnLocationSelected);

            AddLocationCommand = new Command(OnAddLocation);
        }

        async Task ExecuteLoadLocationsCommand()
        {
            IsBusy = true;

            try
            {
                Locations.Clear();
                
                var locations = await DataStore.GetLocationsAsync(true);

                if(locations != null && locations.Any())
                {
                    foreach (var location in locations.OrderBy(x => x.City).ThenBy(x => x.Country))
                    {
                        Locations.Add(location);
                    }
                }
                else
                {
                    Debug.WriteLine("No Locations to show");
                    await Application.Current.MainPage.DisplayAlert("Locations Info", "No Locations to show", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Locations:");
                Debug.WriteLine(ex);
                await Application.Current.MainPage.DisplayAlert("Locations Info", "Failed to Load Locations", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedLocation = null;
        }

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                SetProperty(ref _selectedLocation, value);
                OnLocationSelected(value);
            }
        }

        private async void OnAddLocation(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewLocationPage));
        }

        async void OnLocationSelected(Location location)
        {
            if (location == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(LocationDetailPage)}?{nameof(LocationDetailViewModel.LocationId)}={location.Id}");
        }
    }
}