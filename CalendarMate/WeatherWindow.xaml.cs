﻿using System;
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
using Syncfusion.UI.Xaml.Charts;
using WeatherChartData;
using Normalization;

namespace CalendarMate
{
    public partial class WeatherWindow : Window
    {
        private CurrentWeatherInfoModel currentWeather;
        private DailyWeatherInfoModel dailyWeather;
        private HourlyWeatherInfoModel hourlyWeather;

        // false = 7 day forecast, true = 48 hour forecast
        private bool forecastType = false;

        public WeatherWindow(CurrentWeatherInfoModel weather)
        {
            InitializeComponent();
            currentWeather = weather;
            LoadCurrentWeather();
            SetLegendStyle();
            LoadDayilyTemperatureChart();
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

            CurrentTemperature.Text = "Temperature: " + NormalizationOperations.NormalizeTemperature(currentWeather.Main.Temp).ToString() + " °C";

            CurrentFeelsLikeTemperature.Text = "Apparent temp: " + Math.Round((currentWeather.Main.Feels_like - 273.15), 2).ToString() + " °C";

            CurrentHumidity.Text = "Humidity: " + currentWeather.Main.Humidity.ToString() + " %";
            
            CurrentPressure.Text = "Pressure: " + currentWeather.Main.Pressure.ToString() + " hPa";

            CurrentWindSpeed.Text = "Wind speed: " + currentWeather.Wind.Speed.ToString() + " m/s";
        }


        private void SetLegendStyle()
        {
            // Legend Style
            WeatherChart.Legend = new ChartLegend()
            {
                IconHeight = 10,
                IconWidth = 10,
                Margin = new Thickness(0, 0, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                DockPosition = ChartDock.Top,
                IconVisibility = Visibility.Visible,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Colors.Gray),
                CheckBoxVisibility = Visibility.Visible
            };
        }

        private async void LoadDayilyTemperatureChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather();
            WeatherChart.Series.Clear();
            DisplayedChart.Text = "7 day forecast";
            WeatherChart.Header = "Temperature";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Temperature [°C]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            DailyWeatherForecast dailyData = new DailyWeatherForecast(dailyWeather);

            // Creating AdornmentInfo
            List<ChartAdornmentInfo> adornmentInfoList = new List<ChartAdornmentInfo>();
            for (int i = 0; i < 4; i++)
            {
                ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
                {
                    ShowMarker = true,
                    Symbol = ChartSymbol.Diamond,
                    SymbolHeight = 5,
                    SymbolWidth = 5,
                    SymbolInterior = new SolidColorBrush(Colors.Black),
                    ShowLabel = true,
                    LabelPosition = AdornmentsLabelPosition.Inner,
                    Foreground = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    Background = new SolidColorBrush(Colors.DarkGray),
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(1),
                    FontStyle = FontStyles.Italic,
                    FontFamily = new FontFamily("Calibri"),
                    FontSize = 11
                };
                adornmentInfoList.Add(adornmentInfo);
            }

            //Initialize needed Line Series
            LineSeries seriesMorning = new LineSeries()
            {
                ItemsSource = dailyData.dailyTemperatureForecast.MorningTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Morning",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[0]
            };
            LineSeries seriesDay = new LineSeries()
            {
                ItemsSource = dailyData.dailyTemperatureForecast.DayTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Day",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[1]
            };
            LineSeries seriesEvening = new LineSeries()
            {
                ItemsSource = dailyData.dailyTemperatureForecast.EveningTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Evening",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[2]
            };
            LineSeries seriesNight = new LineSeries()
            {
                ItemsSource = dailyData.dailyTemperatureForecast.NightTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Night",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[3]
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesMorning);
            WeatherChart.Series.Add(seriesDay);
            WeatherChart.Series.Add(seriesEvening);
            WeatherChart.Series.Add(seriesNight);
        }

        private async void LoadDayilyApparentTemperatureChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Apparent Temperature";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Temperature [°C]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            DailyWeatherForecast dailyData = new DailyWeatherForecast(dailyWeather);

            // Creating AdornmentInfo
            List<ChartAdornmentInfo> adornmentInfoList = new List<ChartAdornmentInfo>();
            for (int i = 0; i < 4; i++)
            {
                ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
                {
                    ShowMarker = true,
                    Symbol = ChartSymbol.Diamond,
                    SymbolHeight = 5,
                    SymbolWidth = 5,
                    SymbolInterior = new SolidColorBrush(Colors.Black),
                    ShowLabel = true,
                    LabelPosition = AdornmentsLabelPosition.Inner,
                    Foreground = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    Background = new SolidColorBrush(Colors.DarkGray),
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(1),
                    FontStyle = FontStyles.Italic,
                    FontFamily = new FontFamily("Calibri"),
                    FontSize = 11
                };
                adornmentInfoList.Add(adornmentInfo);
            }

            //Initialize needed Line Series
            LineSeries seriesMorning = new LineSeries()
            {
                ItemsSource = dailyData.dailyApparentTemperatureForecast.MorningApparentTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Morning",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[0]
            };
            LineSeries seriesDay = new LineSeries()
            {
                ItemsSource = dailyData.dailyApparentTemperatureForecast.DayApparentTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Day",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[1]
            };
            LineSeries seriesEvening = new LineSeries()
            {
                ItemsSource = dailyData.dailyApparentTemperatureForecast.EveningApparentTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Evening",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[2]
            };
            LineSeries seriesNight = new LineSeries()
            {
                ItemsSource = dailyData.dailyApparentTemperatureForecast.NightApparentTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Night",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfoList[3]
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesMorning);
            WeatherChart.Series.Add(seriesDay);
            WeatherChart.Series.Add(seriesEvening);
            WeatherChart.Series.Add(seriesNight);
        }

        private async void LoadDayilyHumidityChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Humidity";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Humidity [%]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            DailyWeatherForecast dailyData = new DailyWeatherForecast(dailyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black),
                ShowLabel = true,
                LabelPosition = AdornmentsLabelPosition.Inner,
                Foreground = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Background = new SolidColorBrush(Colors.DarkGray),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(1),
                FontStyle = FontStyles.Italic,
                FontFamily = new FontFamily("Calibri"),
                FontSize = 11
            };

            //Initialize needed Line Series
            LineSeries seriesHumidity = new LineSeries()
            {
                ItemsSource = dailyData.humidity,
                XBindingPath = "Date",
                YBindingPath = "Humidity",
                Label = "Humidity",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesHumidity);
        }

        private async void LoadDayilyPressureChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Pressure";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Pressure [hPa]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            DailyWeatherForecast dailyData = new DailyWeatherForecast(dailyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black),
                ShowLabel = true,
                LabelPosition = AdornmentsLabelPosition.Inner,
                Foreground = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Background = new SolidColorBrush(Colors.DarkGray),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(1),
                FontStyle = FontStyles.Italic,
                FontFamily = new FontFamily("Calibri"),
                FontSize = 11
            };

            //Initialize needed Line Series
            LineSeries seriesPressure = new LineSeries()
            {
                ItemsSource = dailyData.pressure,
                XBindingPath = "Date",
                YBindingPath = "Pressure",
                Label = "Pressure",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesPressure);
        }

        private async void LoadDayilyWindSpeedChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Wind Speed";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Speed [m/s]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            DailyWeatherForecast dailyData = new DailyWeatherForecast(dailyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black),
                ShowLabel = true,
                LabelPosition = AdornmentsLabelPosition.Inner,
                Foreground = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Background = new SolidColorBrush(Colors.DarkGray),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(1),
                FontStyle = FontStyles.Italic,
                FontFamily = new FontFamily("Calibri"),
                FontSize = 11
            };

            //Initialize needed Line Series
            LineSeries seriesWindSpeed = new LineSeries()
            {
                ItemsSource = dailyData.windSpeed,
                XBindingPath = "Date",
                YBindingPath = "WindSpeed",
                Label = "Wind Speed",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesWindSpeed);
        }

        private async void LoadHourlyTemperatureChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather();
            WeatherChart.Series.Clear();
            DisplayedChart.Text = "48 hour forecast";
            WeatherChart.Header = "Temperature";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Temperature [°C]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            HourlyWeatherForecast hourlyData = new HourlyWeatherForecast(hourlyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black)
            };

            //Initialize needed Line Series
            LineSeries seriesTemperature = new LineSeries()
            {
                ItemsSource = hourlyData.temperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Temperature",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesTemperature);
        }

        private async void LoadHourlyApparentTemperatureChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Aparent Temperature";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Temperature [°C]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            HourlyWeatherForecast hourlyData = new HourlyWeatherForecast(hourlyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black)
            };

            //Initialize needed Line Series
            LineSeries seriesApparentTemperature = new LineSeries()
            {
                ItemsSource = hourlyData.apparentTemperature,
                XBindingPath = "Date",
                YBindingPath = "Temperature",
                Label = "Apparent Temperature",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesApparentTemperature);
        }

        private async void LoadHourlyHumidityChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Humidity";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Humidity [%]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            HourlyWeatherForecast hourlyData = new HourlyWeatherForecast(hourlyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black)
            };

            //Initialize needed Line Series
            LineSeries seriesHumidity = new LineSeries()
            {
                ItemsSource = hourlyData.humidity,
                XBindingPath = "Date",
                YBindingPath = "Humidity",
                Label = "Humidity",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesHumidity);
        }

        private async void LoadHourlyPressureChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Presssure";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Pressure [hPa]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            HourlyWeatherForecast hourlyData = new HourlyWeatherForecast(hourlyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black)
            };

            //Initialize needed Line Series
            LineSeries seriesPressure = new LineSeries()
            {
                ItemsSource = hourlyData.pressure,
                XBindingPath = "Date",
                YBindingPath = "Pressure",
                Label = "Pressure",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesPressure);
        }

        private async void LoadHourlyWindSpeedChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather();
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Wind Speed";

            // Adding horizontal axis to chart
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Header = "Date";
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Header = "Speed [m/s]";
            WeatherChart.SecondaryAxis = secondaryAxis;

            // Preparing data for Chart
            HourlyWeatherForecast hourlyData = new HourlyWeatherForecast(hourlyWeather);

            // Creating AdornmentInfo
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black)
            };

            //Initialize needed Line Series
            LineSeries seriesWindSpeed = new LineSeries()
            {
                ItemsSource = hourlyData.windSpeed,
                XBindingPath = "Date",
                YBindingPath = "WindSpeed",
                Label = "Wind Speed",
                LegendIcon = ChartLegendIcon.Circle,
                AdornmentsInfo = adornmentInfo
            };

            //Adding Series to Chart
            WeatherChart.Series.Add(seriesWindSpeed);
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

        private void sevenDayForecast_Click(object sender, RoutedEventArgs e)
        {
            LoadDayilyTemperatureChart();
            forecastType = false;
        }

        private void temperatureForecast_Click(object sender, RoutedEventArgs e)
        {
            if (forecastType)
            {
                LoadHourlyTemperatureChart();
            }
            else
            {
                LoadDayilyTemperatureChart();
            }       
        }

        private void apparentTemperatureForecast_Click(object sender, RoutedEventArgs e)
        {
            if (forecastType)
            {
                LoadHourlyApparentTemperatureChart();
            }
            else
            {
                LoadDayilyApparentTemperatureChart();
            }
        }

        private void humidityForecast_Click(object sender, RoutedEventArgs e)
        {
            if (forecastType)
            {
                LoadHourlyHumidityChart();
            }
            else
            {
                LoadDayilyHumidityChart();
            }
        }

        private void pressureForecast_Click(object sender, RoutedEventArgs e)
        {
            if (forecastType)
            {
                LoadHourlyPressureChart();
            }
            else
            {
                LoadDayilyPressureChart();
            }
        }

        private void windSpeedForecast_Click(object sender, RoutedEventArgs e)
        {
            if (forecastType)
            {
                LoadHourlyWindSpeedChart();
            }
            else
            {
                LoadDayilyWindSpeedChart();
            }
        }

        private void fourtyEightHourForecast_Click(object sender, RoutedEventArgs e)
        {
            LoadHourlyTemperatureChart();
            forecastType = true;
        }
    }
}