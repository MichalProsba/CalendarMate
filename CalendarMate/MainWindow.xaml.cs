using DataBaseEvent.EntityFramework;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Globalization;
using ApiLibrary;
using Normalization;
using System.Threading;



namespace CalendarMate
{
    // The MainWindow class containes window mechanics
    /// <summary>
    /// The <c>MainWindow</c> class.
    /// Containes window mechanics.
    /// </summary>
    public partial class MainWindow : Window
    {
        // The current displayed date
        /// <value>Variable Current_calendar_data holds the current displayed date.</value>
        private CalendarDate Current_calendar_data = new CalendarDate();

        // The currentWeather
        /// <value>Object currentWeather holds the current Weather.</value>
        private CurrentWeatherInfoModel currentWeather = new CurrentWeatherInfoModel();

        // The current date
        /// <value>Variable Current_data holds the current date.</value>
        private DateTime Current_data;

        // The button_list_of_day
        /// <value>Button List button_list_of_day holds the days of month.</value>
        private List<Button> button_list_of_day = new List<Button>();

        // The Button_list_of_day
        /// <value>List Button_list_of_day holds the buttons of current list of day.</value>
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

        // The textblock_list_of_day
        /// <value>Button List textblock_list_of_day holds the days of month number.</value>
        private List<TextBlock> textblock_list_of_day = new List<TextBlock>();

        // The Textblock_list_of_day
        /// <value>List Textblock_list_of_day holds the textblocks of current list of day.</value>
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

        // The textblock_event_list_of_day
        /// <value>Button List textblock_event_list_of_day holds the days of month number.</value>
        private List<Image> image_list_of_day = new List<Image>();

        // The textblock_event_list_of_day
        /// <value>List textblock_event_list_of_day holds the textblocks of current list of day and event.</value>
        public List<Image> Image_list_of_day
        {
            get
            {
                return image_list_of_day;
            }
            set
            {
                image_list_of_day = value;
            }
        }

        // Initializes the main window
        /// <summary>
        /// Initializes the main window.
        /// </summary>
        public MainWindow()
        {
            //Current_calendar_data = new CalendarDate();
            //Current_data = DateTime.Today;
            InitializeComponent();
            GenerateCurrentTime();
            GenerateReminderTime();
            ApiHelper.InitializeClient();
            CityInitialization();
            LoadCurrentCity();
            LoadCurrentWeather();
            //Thread.Sleep(10000);
            Current_data = DateTime.UtcNow.AddSeconds(currentWeather.Timezone);
            GenerateDayPanel();
        }

        // Generates the day panel
        /// <summary>
        /// Generates the day panel 
        /// </summary>
        public void GenerateDayPanel()
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
                Image_list_of_day.Add(new Image());
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

            number_of_day = 1;
            column = day_of_week - 1;
            row = 0;
            
            foreach (Image i in Image_list_of_day)
            {
                i.Name = "Image_" + (number_of_day + 1).ToString();
                i.Visibility = Visibility.Hidden;
                BitmapImage weatherImage= new BitmapImage();
                weatherImage.BeginInit();
                weatherImage.UriSource = new Uri("images/Event.png", UriKind.Relative);
                weatherImage.EndInit();
                i.Source = weatherImage;
                i.Width = 50;
                i.Height = 50;
                i.HorizontalAlignment = HorizontalAlignment.Right;
                i.VerticalAlignment = VerticalAlignment.Bottom;
                i.Margin = new Thickness(0, 0, 10, 10);
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

            AddEventToStackPanel();

            foreach (Button i in Button_list_of_day)
            {
                mainGrid.Children.Add(i);
            }

            foreach (TextBlock i in Textblock_list_of_day)
            {
                mainGrid.Children.Add(i);
            }

            foreach (Image i in Image_list_of_day)
            {
                mainGrid.Children.Add(i);
            }
        }

        // Removes the day panel
        /// <summary>
        /// Removes the day panel 
        /// </summary>
        public void RemoveDayPanel()
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

            foreach (Image i in Image_list_of_day)
            {
                mainGrid.Children.Remove(i);
            }
            Image_list_of_day.Clear();
        }

        // Generates the current time
        /// <summary>
        /// Generates the current time 
        /// </summary>
        private void GenerateCurrentTime()
        {
            DispatcherTimer actualTime = new DispatcherTimer();
            actualTime.Tick += new EventHandler(UpdateCurrentTime);
            actualTime.Interval = new TimeSpan(0, 0, 1);
            actualTime.Start();
        }

        // Updates the current time
        /// <summary>
        /// Updates the current time 
        /// </summary>
        private void UpdateCurrentTime(object sender, EventArgs e)
        {
            if (isCurrentWeatherGood())
            {
                Current_data = DateTime.UtcNow.AddSeconds(currentWeather.Timezone);
                Clock.Text = (Current_data).ToString();
            }
            else
            {
                Clock.Text = DateTime.Now.ToString();
            }
        }

        // Generates the current reminder time
        /// <summary>
        /// Generates the current reminder time 
        /// </summary>
        private void GenerateReminderTime()
        {
            DispatcherTimer actualTime = new DispatcherTimer();
            actualTime.Tick += new EventHandler(UpdateReminderTime);
            actualTime.Interval = new TimeSpan(0, 1, 0);
            actualTime.Start();
        }

        // Updates the current reminder time
        /// <summary>
        /// Updates the current reminder time 
        /// </summary>
        private void UpdateReminderTime(object sender, EventArgs e)
        {
            DataBaseEventDbContext db = new DataBaseEventDbContext();
            var r = from d in db.DataBaseEvents1
                    where d.RemindTime.Year == Current_data.Year && d.RemindTime.Month == Current_data.Month && d.RemindTime.Day == Current_data.Day && d.RemindTime.Hour == Current_data.Hour && d.RemindTime.Minute == Current_data.Minute
                    select d;
            foreach (var item in r)
            {
                string eventReminder = "Event " + item.Name.ToString() + " starts at " + item.StartTime.ToString();
                AlertWindow alertWindow = new AlertWindow(eventReminder, "Reminder");
                alertWindow.ShowDialog();
            }
        }

        // Loads the current weather
        /// <summary>
        /// Loads the current weather
        /// </summary>
        public async void LoadCurrentWeather()
        {
            try
            {
                currentWeather = await CurrentWeatherInfoProcessor.LoadCurrentWeather(ReturnCity());
            }
            catch (Exception)
            {

                Console.WriteLine("Exception occurred");
            }
            
            if (isCurrentWeatherGood())
            {
                BitmapImage weatherImage = new BitmapImage();
                weatherImage.BeginInit();
                weatherImage.UriSource = new Uri("images/" + currentWeather.Weather[0].Icon.ToString() + ".png", UriKind.Relative);
                weatherImage.EndInit();
                CurrentWeatherImage.Source = weatherImage;
                CurrentWeather.Text = NormalizationOperations.NormalizeTemperature(currentWeather.Main.Temp).ToString() + "°C";
                Current_data = DateTime.UtcNow.AddSeconds(currentWeather.Timezone);
                RefreshAllDayButtons();
                ShowCurrentDay();
                AddEventToStackPanel();
            }
            else
            {
                CurrentWeather.Text = "API Error";
            }
        }

        // Loads the next page in the calendar
        /// <summary>
        /// Loads the next page in the calendar 
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.AddMonths(1);
            GenerateDayPanel();
            ShowCurrentDay();
        }

        // Loads the previous page in the calendar
        /// <summary>
        /// Loads the previous page in the calendar 
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.AddMonths(-1);
            GenerateDayPanel();
            ShowCurrentDay();
        }

        // Loads the current date page in the calendar
        /// <summary>
        /// Loads the current date page in the calendar
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CurrentDate_Click(object sender, RoutedEventArgs e)
        {
            RemoveDayPanel();
            Current_calendar_data.SetCurrentDate();
            GenerateDayPanel();
            ShowCurrentDay();
        }

        // Opens a new AddAnEventWindow
        /// <summary>
        /// Opens a new AddAnEventWindow
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Day_Click(object sender, RoutedEventArgs e)
        {
            Current_calendar_data.SetClickDate((Button_list_of_day.IndexOf(((Button)sender))+1)); 
            AddAnEventWindow oneDay = new AddAnEventWindow(Current_calendar_data.Date, this);
            oneDay.ShowDialog();
        }

        // Opens a new ShowAllEventsListWindow
        /// <summary>
        /// Opens a new ShowAllEventsListWindow
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void EventList_Click(object sender, RoutedEventArgs e)
        {
            ShowAllEventsListWindow eventList = new ShowAllEventsListWindow(Current_data, this);
            eventList.ShowDialog();
        }

        // Opens a new ShowToDoListWindow
        /// <summary>
        /// Opens a new ShowToDoListWindow
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void EventToDoList_Click(object sender, RoutedEventArgs e)
        {
            ShowToDoListWindow eventList = new ShowToDoListWindow();
            eventList.ShowDialog();
        }

        // Closes the main window
        /// <summary>
        /// Closes the main window
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CloseMainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Opens a new WeatherWindow
        /// <summary>
        /// Opens a new WeatherWindow
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CurrentWeather_Click(object sender, RoutedEventArgs e)
        {
            if (isCurrentWeatherGood())
            {
                WeatherWindow weatherWindow = new WeatherWindow(currentWeather, this);
                weatherWindow.ShowDialog();
            }
        }

        // Opens a new CurrentCityWindow
        /// <summary>
        /// Opens a new CurrentCityWindow
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void currentCity_Click(object sender, RoutedEventArgs e)
        {
            if (isCurrentWeatherGood())
            {
                CurrentCityWindow currentCityWindow = new CurrentCityWindow(this);
                currentCityWindow.ShowDialog();
            }
        }

        // Refreshes the current weather
        /// <summary>
        /// Refreshes the current weather
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void refreshWeather_Click(object sender, RoutedEventArgs e)
        {
            LoadCurrentWeather();
        }

        // Loads the current city from database
        /// <summary>
        /// Loads the current city  from database
        /// </summary>
        private void CityInitialization()
        {
            DataBaseLocalizationDbContext db = new DataBaseLocalizationDbContext();
            var r = from d in db.DataBaseLocalizations1
                    select d;
            DataBaseLocalization1 obj = r.SingleOrDefault();

            if (obj == null)
            {
                DataBaseLocalization1 doctroObject = new DataBaseLocalization1()
                {
                    Localization = "Wrocław",
                };
                db.DataBaseLocalizations1.Add(doctroObject);
                db.SaveChanges();
            }
        }

        // Returns the current city
        /// <summary>
        /// Returns the current city 
        /// </summary>
        /// <returns>The current city</returns>
        private string ReturnCity()
        {
            DataBaseLocalizationDbContext db = new DataBaseLocalizationDbContext();
            var r = from d in db.DataBaseLocalizations1
                    where d.Id == 1
                    select d;
            DataBaseLocalization1 obj = r.SingleOrDefault();
            return obj.Localization.ToString();
        }

        // Loads the current city
        /// <summary>
        /// Loads the current city 
        /// </summary>
        public void LoadCurrentCity()
        {
            displayedCity.Text = ReturnCity();
        }

        // Minimalizes the main window
        /// <summary>
        /// Minimalizes the main window
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Minimalize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            //this.Hide();
        }

        // Checks if the current weather information is downloaded from API
        /// <summary>
        /// Checks if the current weather information is downloaded from API
        /// </summary>
        /// <returns>True or false depending of the status of the current weather information.</returns>
        private bool isCurrentWeatherGood()
        {
            if (currentWeather.Cod != "404")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Refreshes all day buttons
        /// <summary>
        /// Refreshes all day buttons 
        /// </summary>
        public void RefreshAllDayButtons()
        {
            for (int i = 0; i < Button_list_of_day.Count; i++)
            {
                Button_list_of_day[i].Background = Brushes.DarkGray;
            }
        }

        // Shows current day in dodgerblue
        /// <summary>
        /// Shows current day in dodgerblue 
        /// </summary>
        public void ShowCurrentDay()
        {
            if (Current_data.Year == Current_calendar_data.Date.Year && Current_data.Month == Current_calendar_data.Date.Month)
            {
                Button_list_of_day[Current_data.Day - 1].Background = Brushes.DodgerBlue;
            }
        }

        public void AddEventToStackPanel()
        {
            DataBaseEventDbContext db = new DataBaseEventDbContext();
            var r = from d in db.DataBaseEvents1
                    where d.Year == Current_calendar_data.Date.Year && d.Month == Current_calendar_data.Date.Month
                    select d;

            foreach (var item in r)
            {

                Image_list_of_day[item.Day - 1].Visibility= Visibility.Visible;
            }
        }

        public void RemoveEventToStackPanel(int id)
        {
            DataBaseEventDbContext db = new DataBaseEventDbContext();
            var r = from d in db.DataBaseEvents1
                    where d.Id == id
                    select d;

            foreach (var item in r)
            {
                Image_list_of_day[item.Day - 1].Visibility = Visibility.Hidden;
            }
        }
    }
}
