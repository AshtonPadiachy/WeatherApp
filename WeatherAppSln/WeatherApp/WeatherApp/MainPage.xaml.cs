using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static WeatherApp.WeatherInfo;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //var response = await GetWeatherInfo();

           // BindingContext = response.main;  

           BindingContext = await GetWeatherInfo();
        }

        private async Task<OpenWeatherInfo>GetWeatherInfo()
        {

            var details = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (details != PermissionStatus.Granted)
            {
                var newdetail = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            var location = await Geolocation.GetLocationAsync();

            var latitude= location.Latitude;
            var longitude= location.Longitude;

           // var location = Geolocation.GetLocationAsync();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units=metric&appid=b6da8fe4c36aa7ecc16ea9a9fd68abc1";

            var response = await client.GetStringAsync(url);

            var weatherInfo = JsonConvert.DeserializeObject<OpenWeatherInfo>(response);

            return weatherInfo;
        }
    }
}
