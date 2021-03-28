using ApiLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace CalendarMate
{
    /// <summary>
    /// Logika interakcji dla klasy Calendar.xaml
    /// </summary>
    public partial class Calendar : Window
    {
        private CalendarDate Current_calendar_data = new CalendarDate();

        private List<Button> button_list_of_day = new List<Button>();
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

        public Calendar()
        {
            //Current_calendar_data = new CalendarDate();
            //InitializeComponent();
            //GenerateCurrentTime();
            //GenerateDayPanel();
            //ApiHelper.InitializeClient();
            //LoadCurrentWeather();
        }
        private void GenerateDayPanel()
        {
            Month_And_Year_TextBlock.Text = Current_calendar_data.Date.ToString("Y", CultureInfo.CreateSpecificCulture("en-US"));
            int number_of_days = Current_calendar_data.DaysInCalendarMonth();
            int day_of_week = Current_calendar_data.FirstDayOfWeekCalendarMonth();

            int number_of_day = 1;
            int column = day_of_week;
            int row = 3;

            for (int i = 0; i < number_of_days; i++)
            {
                Button_list_of_day.Add(new Button());
                Textblock_list_of_day.Add(new TextBlock());
            }

            foreach (Button i in Button_list_of_day)
            {
                i.Name = "Button_" + number_of_day.ToString();
                i.Click += new RoutedEventHandler(Day_Click);
                i.Background = Brushes.DarkGray;
                Grid.SetColumn(i, column);
                Grid.SetRow(i, row);
                if (column % 7 == 0)
                {
                    row++;
                    column = 0;
                }
                column++;
                number_of_day++;
            }

            number_of_day = 1;
            column = day_of_week;
            row = 3;
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
                if (column % 7 == 0)
                {
                    row++;
                    column = 0;
                }
                column++;
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

        private async void LoadCurrentWeather()
        {
            var weather = await WeatherProcessor.LoadCurrentWeather();
            Weather.Content = Math.Round((weather.Temp - 273.15), 2).ToString() + "°C";
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
            AddAnEventWindow2 oneDay = new AddAnEventWindow2(Current_calendar_data.Date);
            oneDay.Show();
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

        private void Weather_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
