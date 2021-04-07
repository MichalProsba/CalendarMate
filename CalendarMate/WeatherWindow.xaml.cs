using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ApiLibrary;

namespace CalendarMate
{
    public partial class WeatherWindow : Window
    {
        private CurrentWeatherInfoModel currentWeather;
        private DailyWeatherInfoModel dailyWeather;
        private HourlyWeatherInfoModel hourlyWeather;
        public WeatherWindow(CurrentWeatherInfoModel weather)
        {
            InitializeComponent();
            currentWeather = weather;
            LoadCurrentWeather();
            LoadDailyWeather();
        }

        private void LoadCurrentWeather()
        {
            CurrentWeatherName.Text = currentWeather.Name;
            BitmapImage weatherImage = new BitmapImage();
            weatherImage.BeginInit();
            weatherImage.UriSource = new Uri("images/" + currentWeather.Weather[0].Icon.ToString() + ".png", UriKind.Relative);
            weatherImage.EndInit();
            CurrentWeatherImage.Source = weatherImage;

            CurrentWeatherDescription.Text = currentWeather.Weather[0].Description.ToString();

            CurrentTemperature.Text = "Temperature: " + Math.Round((currentWeather.Main.Temp - 273.15), 2).ToString() + " °C";

            CurrentFeelsLikeTemperature.Text = "Apparent temp: " + Math.Round((currentWeather.Main.Feels_like - 273.15), 2).ToString() + " °C";

            CurrentHumidity.Text = "Humidity: " + currentWeather.Main.Humidity.ToString() + " %";
            
            CurrentPressure.Text = "Pressure: " + currentWeather.Main.Pressure.ToString() + " hPa";

            CurrentWindSpeed.Text = "Wind speed: " + currentWeather.Wind.Speed.ToString() + " m/s";
        }

        private async void LoadDailyWeather()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather();
            WeatherChart.FontSize = 30;
            WeatherChart.Header = "7 day forecast";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
