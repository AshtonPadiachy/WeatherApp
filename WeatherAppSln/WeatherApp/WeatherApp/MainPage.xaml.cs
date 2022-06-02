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

        private async void GetWeatherInfo()
        {
            var location = Geolocation.GetLocationAsync();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=35&lon=139&appid=b6da8fe4c36aa7ecc16ea9a9fd68abc1");

            var weatherInfo = JsonConvert.DeserializeObject<OpenWeatherInfo>(response);
        }
    }
}
