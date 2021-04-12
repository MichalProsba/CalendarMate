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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Globalization;
using ApiLibrary;
using Normalization;



namespace CalendarMate
{
    public partial class MainWindow : Window
    {
        private CalendarDate Current_calendar_data = new CalendarDate();

        private List<Button> button_list_of_day = new List<Button>();
        private CurrentWeatherInfoModel currentWeather;
        public List<Button> Button_list_of_day
        {
            get
            {
                return button_list_of_day;
            }
            set
            {
                button_list_of_day = value;
            }
        }

        private List<TextBlock> textblock_list_of_day = new List<TextBlock>();

        public List<TextBlock> Textblock_list_of_day
        {
            get
            {
                return textblock_list_of_day;
            }
            set
            {
                textblock_list_of_day = value;
            }
        }

        public MainWindow()
        {
            Current_calendar_data = new CalendarDate();
            InitializeComponent();
            GenerateCurrentTime();
            GenerateDayPanel();
            ApiHelper.InitializeClient();
            LoadCurrentWeather();
        }
        private void GenerateDayPanel()
        {
            Month_And_Year_TextBlock.Text = Current_calendar_data.Date.ToString("Y", CultureInfo.CreateSpecificCulture("en-US"));
            int number_of_days = Current_calendar_data.DaysInCalendarMonth();
            int day_of_week = Current_calendar_data.FirstDayOfWeekCalendarMonth();

            int number_of_day = 1;
            int column = day_of_week-1;
            int row = 0;

            for (int i = 0; i < number_of_days; i++)
            {
                Button_list_of_day.Add(new Button());
                Textblock_list_of_day.Add(new TextBlock());
            }

            foreach (Button i in Button_list_of_day)
            {

                i.Name = "Button_" + (number_of_day + 1).ToString();
                i.Click += new RoutedEventHandler(Day_Click);
                i.Background = Brushes.DarkGray;
                i.Height = 140;
                Grid.SetColumn(i, column);
                Grid.SetRow(i, row);
                column++;
                if (column % 7 == 0)
                {
                    row++;
                    column = 0;
                }
                number_of_day++;
            }

            number_of_day = 1;
            column = day_of_week-1;
            row = 0;
            foreach (TextBlock i in Textblock_list_of_day)
            {
                i.Name = "TextBlock_" + (number_of_day + 1).ToString();
                i.Text = number_of_day.ToString();
                i.HorizontalAlignment = HorizontalAlignment.Left;
                i.VerticalAlignment = VerticalAlignment.Top;
                i.Margin = new Thickness(5, 0, 0, 0);
                i.FontSize = 20;
                i.Foreground = Brushes.White;
                Grid.SetColumn(i, column);
                Grid.SetRow(i, row);
                column++;
                if (column % 7 == 0)
                {
                    row++;
                    column = 0;
                }
                number_of_day++;
            }

            foreach (Button i in Button_list_of_day)
            {
                mainGrid.Children.Add(i);
            }

            foreach (TextBlock i in Textblock_list_of_day)
            {
                mainGrid.Children.Add(i);
            }
        }

        private void RemoveDayPanel()
        {
            foreach (Button i in Button_list_of_day)
            {
                mainGrid.Children.Remove(i);
            }
            Button_list_of_day.Clear();

            foreach (TextBlock i in Textblock_list_of_day)
            {
                mainGrid.Children.Remove(i);
            }
            Textblock_list_of_day.Clear();
        }

        private void GenerateCurrentTime()
        {
            DispatcherTimer actualTime = new DispatcherTimer();
            actualTime.Tick += new EventHandler(UpdateCurrentTime);
            actualTime.Interval = new TimeSpan(0, 0, 1);
            actualTime.Start();
        }
        private void UpdateCurrentTime(object sender, EventArgs e)
        {
            Clock.Content = DateTime.Now.ToString();
        }

        public async void LoadCurrentWeather()
        {
            currentWeather = await CurrentWeatherInfoProcessor.LoadCurrentWeather();
            BitmapImage weatherImage = new BitmapImage();
            weatherImage.BeginInit();
            weatherImage.UriSource = new Uri("images/" + currentWeather.Weather[0].Icon.ToString() + ".png", UriKind.Relative);
            weatherImage.EndInit();
            CurrentWeatherImage.Source = weatherImage;
            CurrentWeather.Text = NormalizationOperations.NormalizeTemperature(currentWeather.Main.Temp).ToString() + "°C";
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.AddMonths(1);
            GenerateDayPanel();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.AddMonths(-1);
            GenerateDayPanel();
        }
        
        private void CurrentDate_Click(object sender, RoutedEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.SetCurrentDate();
            GenerateDayPanel();
        }

        private void Day_Click(object sender, RoutedEventArgs e)
        {
            Current_calendar_data.SetClickDate((Button_list_of_day.IndexOf(((Button)sender))+1)); 
            AddAnEventWindow oneDay = new AddAnEventWindow(Current_calendar_data.Date);
            oneDay.ShowDialog();
        }
        private void CloseMainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Calendar_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.AddMonths(1);
            GenerateDayPanel();
        }

        private void Calendar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.AddMonths(-1);
            GenerateDayPanel();
        }

        private void CurrentWeather_Click(object sender, RoutedEventArgs e)
        {
            WeatherWindow weatherWindow = new WeatherWindow(currentWeather, this);
            weatherWindow.ShowDialog();
        }

        private void currentCity_Click(object sender, RoutedEventArgs e)
        {
            CurrentCityWindow currentCityWindow = new CurrentCityWindow();
            currentCityWindow.ShowDialog();
        }

        private void refreshWeather_Click(object sender, RoutedEventArgs e)
        {
            LoadCurrentWeather();
        }
    }
}
