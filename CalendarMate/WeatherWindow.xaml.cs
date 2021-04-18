using DataBaseEvent.Domain.Models;
using DataBaseLocalization.EntityFramework;
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
using Syncfusion.UI.Xaml.Charts;
using WeatherChartData;
using Normalization;

namespace CalendarMate
{
    // The WeatherWindow class containes methods and variables necessary to create and manage the Weather Window
    /// <summary>
    /// The <c>WeatherWindow</c> class.
    /// Containes methods and variables necessary to create and manage the Weather Window.
    /// </summary>
    public partial class WeatherWindow : Window
    {
        // The current weather
        /// <value>Gets and Sets the current weather information.</value>
        private CurrentWeatherInfoModel currentWeather;

        // The daily weather
        /// <value>Gets and Sets the daily weather information.</value>
        private DailyWeatherInfoModel dailyWeather;

        // The hourly weather
        /// <value>Gets and Sets the hourly weather information.</value>
        private HourlyWeatherInfoModel hourlyWeather;

        // MainWindow object to store the reference to our main window
        /// <summary>
        /// MainWindow object to store the reference to our main window.
        /// </summary>
        MainWindow mainWindow;

        // A bool value that containes the current displayed forecast type
        // false = 7 day forecast, true = 48 hour forecast
        /// <summary>
        /// A bool value that containes the current displayed forecast type .
        /// false = 7 day forecast, true = 48 hour forecast.
        /// </summary>
        private bool forecastType = false;

        // CategoryAxis object to store the x axis information
        /// <summary>
        /// CategoryAxis object to store the x axis information.
        /// </summary>
        CategoryAxis primaryAxis;

        // NumericalAxis object to store the y axis information
        /// <summary>
        /// NumericalAxis object to store the y axis information.
        /// </summary>
        NumericalAxis secondaryAxis;

        // The WeatherWindow constructor
        /// <summary>
        /// The WeatherWindow constructor. 
        /// </summary>
        /// <param name="weather">CurrentWeatherInfoModel object containing current weather information.</param>
        /// <param name="main">MainWindow object cotaining the reference to our main window.</param>
        public WeatherWindow(CurrentWeatherInfoModel weather, MainWindow main)
        {
            InitializeComponent();

            currentWeather = weather;
            mainWindow = main;

            LoadCurrentWeather();
            SetChartStyle();
            LoadDayilyTemperatureChart();
        }

        // Loads the current weather
        /// <summary>
        /// Loads the current weather.
        /// </summary>
        private async void LoadCurrentWeather()
        {
            mainWindow.LoadCurrentWeather();
            currentWeather = await CurrentWeatherInfoProcessor.LoadCurrentWeather(ReturnCity());
            BitmapImage weatherImage = new BitmapImage();
            weatherImage.BeginInit();
            weatherImage.UriSource = new Uri("images/" + currentWeather.Weather[0].Icon.ToString() + ".png", UriKind.Relative);
            weatherImage.EndInit();

            CurrentWeatherImage.Source = weatherImage;

            CurrentWeatherName.Text = currentWeather.Name;

            CurrentWeatherDescription.Text = currentWeather.Weather[0].Description.ToString();

            CurrentTemperature.Text = "Temperature: " + NormalizationOperations.NormalizeTemperature(currentWeather.Main.Temp).ToString() + " °C";

            CurrentFeelsLikeTemperature.Text = "Apparent temp: " + Math.Round((currentWeather.Main.Feels_like - 273.15), 2).ToString() + " °C";

            CurrentHumidity.Text = "Humidity: " + currentWeather.Main.Humidity.ToString() + " %";
            
            CurrentPressure.Text = "Pressure: " + currentWeather.Main.Pressure.ToString() + " hPa";

            CurrentWindSpeed.Text = "Wind speed: " + currentWeather.Wind.Speed.ToString() + " m/s";
        }

        // Sets the charts style
        /// <summary>
        /// Sets the charts style.
        /// </summary>
        private void SetChartStyle()
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

            // Adding zooming to chart
            ChartZoomPanBehavior zooming = new ChartZoomPanBehavior()
            {
                EnableMouseWheelZooming = true,
                EnableZoomingToolBar = true,
                ToolBarBackground = new SolidColorBrush(Colors.Black),
                ToolBarItemHeight = 15,
                ToolBarItemWidth = 15,
                ToolBarItemMargin = new Thickness(10),
                ToolBarItems = ZoomToolBarItems.Reset
            };
            WeatherChart.Behaviors.Add(zooming);

            // Adding horizontal axis to chart
            primaryAxis = new CategoryAxis();
            WeatherChart.PrimaryAxis = primaryAxis;


            // Adding vertical axis to chart
            secondaryAxis = new NumericalAxis();
            WeatherChart.SecondaryAxis = secondaryAxis;
        }

        // Loads the daily temperature chart
        /// <summary>
        /// Loads the daily temperature chart.
        /// </summary>
        private async void LoadDayilyTemperatureChart()
        {
            sevenDayForecast.IsEnabled = false;
            temperatureForecast.IsEnabled = false;

            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            DisplayedChart.Text = "7 day forecast";
            WeatherChart.Header = "Temperature";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Temperature [°C]";

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

        // Loads the daily apparent temperature chart
        /// <summary>
        /// Loads the daily apparent temperature chart.
        /// </summary>
        private async void LoadDayilyApparentTemperatureChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Apparent Temperature";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Temperature [°C]";

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

        // Loads the daily humidity chart
        /// <summary>
        /// Loads the daily humidity chart.
        /// </summary>
        private async void LoadDayilyHumidityChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Humidity";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Humidity [%]";

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

        // Loads the daily pressure chart
        /// <summary>
        /// Loads the daily pressure chart.
        /// </summary>
        private async void LoadDayilyPressureChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Pressure";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Pressure [hPa]";

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

        // Loads the daily wind speed chart
        /// <summary>
        /// Loads the daily wind speed chart.
        /// </summary>
        private async void LoadDayilyWindSpeedChart()
        {
            dailyWeather = await DailyWeatherInfoProcessor.LoadDailyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Wind Speed";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Speed [m/s]";

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

        // Loads the hourly temperature chart
        /// <summary>
        /// Loads the hourly temperature chart.
        /// </summary>
        private async void LoadHourlyTemperatureChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            DisplayedChart.Text = "48 hour forecast";
            WeatherChart.Header = "Temperature";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Temperature [°C]";

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

        // Loads the hourly apparent temperature chart
        /// <summary>
        /// Loads the hourly apparent temperature chart.
        /// </summary>
        private async void LoadHourlyApparentTemperatureChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Aparent Temperature";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Temperature [°C]";

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

        // Loads the hourly humidity chart
        /// <summary>
        /// Loads the hourly humidity chart
        /// </summary>
        private async void LoadHourlyHumidityChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Humidity";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Humidity [%]";

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

        // Loads the hourly pressure chart
        /// <summary>
        /// Loads the hourly pressure chart.
        /// </summary>
        private async void LoadHourlyPressureChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Presssure";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Pressure [hPa]";

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

        // Loads the hourly wind speed chart
        /// <summary>
        /// Loads the hourly wind speed chart.
        /// </summary>
        private async void LoadHourlyWindSpeedChart()
        {
            hourlyWeather = await HourlyWeatherInfoProcessor.LoadHourlyWeather(currentWeather.Coord.Lon, currentWeather.Coord.Lat);
            WeatherChart.Series.Clear();
            WeatherChart.Header = "Wind Speed";

            // Adding horizontal axis to chart
            primaryAxis.Header = "Date";


            // Adding vertical axis to chart
            secondaryAxis.Header = "Speed [m/s]";

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

        // Closes the current window
        /// <summary>
        /// Closes the current window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Alows to drag the current window
        /// <summary>
        /// Alows to drag the current window .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        // Changes current chart to seven day temperature forecast chart
        /// <summary>
        /// Changes current chart to seven day temperature forecast chart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sevenDayForecast_Click(object sender, RoutedEventArgs e)
        {
            sevenDayForecast.IsEnabled = false;
            fourtyEightHourForecast.IsEnabled = true;

            temperatureForecast.IsEnabled = false;
            apparentTemperatureForecast.IsEnabled = true;
            humidityForecast.IsEnabled = true;
            pressureForecast.IsEnabled = true;
            windSpeedForecast.IsEnabled = true;

            LoadDayilyTemperatureChart();
            forecastType = false;
        }

        // Changes current chart to daily or hourly (depending on the forecastType value) temperature forecast chart
        /// <summary>
        /// Changes current chart to daily or hourly (depending on the <c>forecastType</c> value) temperature forecast chart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void temperatureForecast_Click(object sender, RoutedEventArgs e)
        {
            temperatureForecast.IsEnabled = false;
            apparentTemperatureForecast.IsEnabled = true;
            humidityForecast.IsEnabled = true;
            pressureForecast.IsEnabled = true;
            windSpeedForecast.IsEnabled = true;
            if (forecastType)
            {
                LoadHourlyTemperatureChart();
            }
            else
            {
                LoadDayilyTemperatureChart();
            }       
        }

        // Changes current chart to daily or hourly (depending on the forecastType value) apparent temperature forecast chart
        /// <summary>
        /// Changes current chart to daily or hourly (depending on the <c>forecastType</c> value) apparent temperature forecast chart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apparentTemperatureForecast_Click(object sender, RoutedEventArgs e)
        {
            temperatureForecast.IsEnabled = true;
            apparentTemperatureForecast.IsEnabled = false;
            humidityForecast.IsEnabled = true;
            pressureForecast.IsEnabled = true;
            windSpeedForecast.IsEnabled = true;
            if (forecastType)
            {
                LoadHourlyApparentTemperatureChart();
            }
            else
            {
                LoadDayilyApparentTemperatureChart();
            }
        }

        // Changes current chart to daily or hourly (depending on the forecastType value) humidity forecast chart
        /// <summary>
        /// Changes current chart to daily or hourly (depending on the <c>forecastType</c> value) humidity forecast chart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void humidityForecast_Click(object sender, RoutedEventArgs e)
        {
            temperatureForecast.IsEnabled = true;
            apparentTemperatureForecast.IsEnabled = true;
            humidityForecast.IsEnabled = false;
            pressureForecast.IsEnabled = true;
            windSpeedForecast.IsEnabled = true;
            if (forecastType)
            {
                LoadHourlyHumidityChart();
            }
            else
            {
                LoadDayilyHumidityChart();
            }
        }

        // Changes current chart to daily or hourly (depending on the forecastType value) pressure forecast chart
        /// <summary>
        /// Changes current chart to daily or hourly (depending on the <c>forecastType</c> value) pressure forecast chart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pressureForecast_Click(object sender, RoutedEventArgs e)
        {
            temperatureForecast.IsEnabled = true;
            apparentTemperatureForecast.IsEnabled = true;
            humidityForecast.IsEnabled = true;
            pressureForecast.IsEnabled = false;
            windSpeedForecast.IsEnabled = true;
            if (forecastType)
            {
                LoadHourlyPressureChart();
            }
            else
            {
                LoadDayilyPressureChart();
            }
        }

        // Changes current chart to daily or hourly (depending on the forecastType value) wind speed forecast chart
        /// <summary>
        /// Changes current chart to daily or hourly (depending on the <c>forecastType</c> value) wind speed forecast chart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void windSpeedForecast_Click(object sender, RoutedEventArgs e)
        {
            temperatureForecast.IsEnabled = true;
            apparentTemperatureForecast.IsEnabled = true;
            humidityForecast.IsEnabled = true;
            pressureForecast.IsEnabled = true;
            windSpeedForecast.IsEnabled = false;
            if (forecastType)
            {
                LoadHourlyWindSpeedChart();
            }
            else
            {
                LoadDayilyWindSpeedChart();
            }
        }

        // Changes current chart to 48 hour temperature forecast chart
        /// <summary>
        /// Changes current chart to 48 hour temperature forecast chart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fourtyEightHourForecast_Click(object sender, RoutedEventArgs e)
        {
            sevenDayForecast.IsEnabled = true;
            fourtyEightHourForecast.IsEnabled = false;

            temperatureForecast.IsEnabled = false;
            apparentTemperatureForecast.IsEnabled = true;
            humidityForecast.IsEnabled = true;
            pressureForecast.IsEnabled = true;
            windSpeedForecast.IsEnabled = true;

            LoadHourlyTemperatureChart();
            forecastType = true;
        }

        // Refreshes the current weather
        /// <summary>
        /// Refreshes the current weather.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshWeather_Click(object sender, RoutedEventArgs e)
        {
            LoadCurrentWeather();
        }

        // Returns the current city from database
        /// <summary>
        /// Returns the current city from database.
        /// </summary>
        /// <returns></returns>
        private string ReturnCity()
        {
            DataBaseLocalizationDbContext db = new DataBaseLocalizationDbContext();
            var r = from d in db.DataBaseLocalizations1
                    where d.Id == 1
                    select d;
            DataBaseLocalization1 obj = r.SingleOrDefault();
            return obj.Localization.ToString();
        }
    }
}
