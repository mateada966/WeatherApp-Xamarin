using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Models.Weather;
using WeatherApp.Services.WeatherAPI;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    [QueryProperty(nameof(LocationId), nameof(LocationId))]
    public class LocationDetailViewModel : BaseViewModel
    {
        #region LocationId
        // LocationId
        //-----------------------------------------------------------
        private string locationId;

        public string LocationId
        {
            get
            {
                return locationId;
            }
            set
            {
                locationId = value;
                LoadLocationId(value);
            }
        }
        //-----------------------------------------------------------
        #endregion

        #region Id, City, Country
        // Id, City, Country
        //-----------------------------------------------------------
        public string Id { get; set; }

        private string city;
        public string City
        {
            get => city;
            set => SetProperty(ref city, value);
        }

        private string country;
        public string Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }
        //-----------------------------------------------------------
        #endregion

        #region Current Weather
        // Current Weather
        //-----------------------------------------------------------
        private string cityTxt;
        public string CityTxt
        {
            get => cityTxt;
            set => SetProperty(ref cityTxt, value);
        }
        private string iconImg;
        public string IconImg
        {
            get => iconImg;
            set => SetProperty(ref iconImg, value);
        }

        private string temperatureTxt;
        public string TemperatureTxt
        {
            get => temperatureTxt;
            set => SetProperty(ref temperatureTxt, value);
        }

        private string descriptionTxt;
        public string DescriptionTxt
        {
            get => descriptionTxt;
            set => SetProperty(ref descriptionTxt, value);
        }

        private string dateTxt;
        public string DateTxt
        {
            get => dateTxt;
            set => SetProperty(ref dateTxt, value);
        }

        private string humidityTxt;
        public string HumidityTxt
        {
            get => humidityTxt;
            set => SetProperty(ref humidityTxt, value);
        }

        private string windTxt;
        public string WindTxt
        {
            get => windTxt;
            set => SetProperty(ref windTxt, value);
        }

        private string pressureTxt;
        public string PressureTxt
        {
            get => pressureTxt;
            set => SetProperty(ref pressureTxt, value);
        }

        private string cloudinessTxt;
        public string CloudinessTxt
        {
            get => cloudinessTxt;
            set => SetProperty(ref cloudinessTxt, value);
        }
        //-----------------------------------------------------------
        #endregion

        #region Forecast Weather
        // Forecast Weather
        //-----------------------------------------------------------
        #region DayOne
        private string dayOneTxt;
        public string DayOneTxt
        {
            get => dayOneTxt;
            set => SetProperty(ref dayOneTxt, value);
        }

        private string dateOneTxt;
        public string DateOneTxt
        {
            get => dateOneTxt;
            set => SetProperty(ref dateOneTxt, value);
        }

        private string iconOneTxt;
        public string IconOneTxt
        {
            get => iconOneTxt;
            set => SetProperty(ref iconOneTxt, value);
        }

        private string tempOneTxt;
        public string TempOneTxt
        {
            get => tempOneTxt;
            set => SetProperty(ref tempOneTxt, value);
        }
        #endregion

        #region DayTwo
        private string dayTwoTxt;
        public string DayTwoTxt
        {
            get => dayTwoTxt;
            set => SetProperty(ref dayTwoTxt, value);
        }

        private string dateTwoTxt;
        public string DateTwoTxt
        {
            get => dateTwoTxt;
            set => SetProperty(ref dateTwoTxt, value);
        }

        private string iconTwoTxt;
        public string IconTwoTxt
        {
            get => iconTwoTxt;
            set => SetProperty(ref iconTwoTxt, value);
        }

        private string tempTwoTxt;
        public string TempTwoTxt
        {
            get => tempTwoTxt;
            set => SetProperty(ref tempTwoTxt, value);
        }
        #endregion

        #region DayThree
        private string dayThreeTxt;
        public string DayThreeTxt
        {
            get => dayThreeTxt;
            set => SetProperty(ref dayThreeTxt, value);
        }

        private string dateThreeTxt;
        public string DateThreeTxt
        {
            get => dateThreeTxt;
            set => SetProperty(ref dateThreeTxt, value);
        }

        private string iconThreeTxt;
        public string IconThreeTxt
        {
            get => iconThreeTxt;
            set => SetProperty(ref iconThreeTxt, value);
        }

        private string tempThreeTxt;
        public string TempThreeTxt
        {
            get => tempThreeTxt;
            set => SetProperty(ref tempThreeTxt, value);
        }
        #endregion

        #region DayFour
        private string dayFourTxt;
        public string DayFourTxt
        {
            get => dayFourTxt;
            set => SetProperty(ref dayFourTxt, value);
        }

        private string dateFourTxt;
        public string DateFourTxt
        {
            get => dateFourTxt;
            set => SetProperty(ref dateFourTxt, value);
        }

        private string iconFourTxt;
        public string IconFourTxt
        {
            get => iconFourTxt;
            set => SetProperty(ref iconFourTxt, value);
        }

        private string tempFourTxt;
        public string TempFourTxt
        {
            get => tempFourTxt;
            set => SetProperty(ref tempFourTxt, value);
        }
        #endregion
        //-----------------------------------------------------------
        #endregion

        #region API key
        // API key
        //-----------------------------------------------------------
        private string apiKey;
        public string APIKey
        {
            get => apiKey;
            set => SetProperty(ref apiKey, value);
        }
        //-----------------------------------------------------------
        #endregion

        public LocationDetailViewModel()
        {
            APIKey = API_Key;
        }

        public async void LoadLocationId(string locationId)
        {
            try
            {
                var location = await DataStore.GetLocationAsync(locationId);
                Id = location.Id;
                City = location.City;
                Country = location.Country;

                GetWeatherInfo();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Location:");
                Debug.WriteLine(ex);
                await Application.Current.MainPage.DisplayAlert("Location Info", "Failed to Load Location", "OK");
            }
        }

        internal async void DeleteLocation()
        {
            await DataStore.DeleteLocationAsync(Id);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async void GetWeatherInfo()
        {
            var url = "";

            if(!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
            {
                url = $"http://api.openweathermap.org/data/2.5/weather?q={City},{Country}&appid={APIKey}&units=metric";
            }
            else if(!string.IsNullOrEmpty(City))
            {
                url = $"http://api.openweathermap.org/data/2.5/weather?q={City}&appid={APIKey}&units=metric";
            }

            var result = await APICaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);

                    CityTxt = string.Concat(weatherInfo.name.ToUpper(), ", ", weatherInfo.sys.country);

                    IconImg = $"w{weatherInfo.weather[0].icon}";
                    TemperatureTxt = weatherInfo.main.temp.ToString("0");

                    DescriptionTxt = weatherInfo.weather[0].description.ToUpper();
                    var dt = DateTime.Now;
                    DateTxt = dt.ToString("dddd, dd MMMM - HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")).ToUpper();

                    HumidityTxt = $"{weatherInfo.main.humidity}%";
                    WindTxt = $"{weatherInfo.wind.speed} m/s";
                    PressureTxt = $"{weatherInfo.main.pressure} hPa";
                    CloudinessTxt = $"{weatherInfo.clouds.all}%";

                    GetForecastInfo(dt);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Weather Info", "No WEATHER information found!", "OK");
            }
        }

        public async void GetForecastInfo(DateTime dt)
        {
            var url = "";

            if (!string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(Country))
            {
                url = $"http://api.openweathermap.org/data/2.5/forecast?q={City},{Country}&appid={APIKey}&units=metric";
            }
            else if (!string.IsNullOrEmpty(City))
            {
                url = $"http://api.openweathermap.org/data/2.5/forecast?q={City}&appid={APIKey}&units=metric";
            }

            var result = await APICaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();

                    var dtDateNow = dt.ToString("yyyy-MM-dd");

                    foreach (var list in forcastInfo.list)
                    {
                        var date = DateTime.Parse(list.dt_txt);

                        if (date.ToString("yyyy-MM-dd").CompareTo(dtDateNow) > 0
                            && date.ToString("HH-mm-ss").CompareTo("12-00-00") == 0)
                        {
                            allList.Add(list);
                        }
                    }

                    //Day 1
                    DayOneTxt = DateTime.Parse(allList[0].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    DateOneTxt = DateTime.Parse(allList[0].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    IconOneTxt = $"w{allList[0].weather[0].icon}";
                    TempOneTxt = allList[0].main.temp.ToString("0");

                    //Day 2
                    DayTwoTxt = DateTime.Parse(allList[1].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    DateTwoTxt = DateTime.Parse(allList[1].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    IconTwoTxt = $"w{allList[1].weather[0].icon}";
                    TempTwoTxt = allList[1].main.temp.ToString("0");

                    //Day 3
                    DayThreeTxt = DateTime.Parse(allList[2].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    DateThreeTxt = DateTime.Parse(allList[2].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    IconThreeTxt = $"w{allList[2].weather[0].icon}";
                    TempThreeTxt = allList[2].main.temp.ToString("0");

                    //Day 4
                    DayFourTxt = DateTime.Parse(allList[3].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    DateFourTxt = DateTime.Parse(allList[3].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    IconFourTxt = $"w{allList[3].weather[0].icon}";
                    TempFourTxt = allList[3].main.temp.ToString("0");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Weather Info", "No FORECAST information found!", "OK");
            }
        }
    }
}
