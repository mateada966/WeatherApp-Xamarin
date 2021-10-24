using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models.Weather;
using WeatherApp.Services.WeatherAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentPositionPage : ContentPage
    {
        private string Location = "Gliwice";
        private string APIKey = "c889a0af65c88a43107431a60ba35d84";

        public CurrentPositionPage()
        {
            InitializeComponent();
            GetWeatherInfo();
        }

        private async void GetWeatherInfo()
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={Location}&appid={APIKey}&units=metric";

            var result = await APICaller.Get(url);

            if(result.Successful)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);

                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                    iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    var dt = DateTime.Now;
                    dateTxt.Text = dt.ToString("dddd, dd MMMM - HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")).ToUpper();

                    GetForecast();
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found!", "OK");
            }
        }

        private async void GetForecast()
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q={Location}&appid={APIKey}&units=metric";
            var result = await APICaller.Get(url);

            if (result.Successful)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();

                    foreach (var list in forcastInfo.list)
                    {
                        //var date = DateTime.ParseExact(list.dt_txt, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 12 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    //Day 1
                    dayOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    dateOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    iconOneImg.Source = $"w{allList[0].weather[0].icon}";
                    tempOneTxt.Text = allList[0].main.temp.ToString("0");

                    //Day 2
                    dayTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    dateTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    iconTwoImg.Source = $"w{allList[1].weather[0].icon}";
                    tempTwoTxt.Text = allList[1].main.temp.ToString("0");

                    //Day 3
                    dayThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    dateThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    iconThreeImg.Source = $"w{allList[2].weather[0].icon}";
                    tempThreeTxt.Text = allList[2].main.temp.ToString("0");

                    //Day 4
                    dayFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd", CultureInfo.CreateSpecificCulture("en-US"));
                    dateFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMMM", CultureInfo.CreateSpecificCulture("en-US"));
                    iconFourImg.Source = $"w{allList[3].weather[0].icon}";
                    tempFourTxt.Text = allList[3].main.temp.ToString("0");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            GetWeatherInfo();
        }
    }
}
