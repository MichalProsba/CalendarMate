using DataBaseEvent.Domain.Models;
using DataBaseEvent.EntityFramework;
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

namespace CalendarMate
{
    // The ShowAnEventWindow class containes window mechanics displaying events from data base
    /// <summary>
    /// The <c> ShowAnEventWindow </c> class.
    /// Containes window mechanics displaying events from data base.
    /// </summary>
    public partial class ShowAnEventWindow : Window
    {
        // The id number
        /// <value> Variable updatingEventID holds the current id </value>
        private int updatingEventID = 0;

        // The date selected by the user
        /// <value> Variable EventDate holds selected by the user date </value>
        private DateTime EventDate;

        /// <summary>
        ///  Contain MainWindow object
        /// </summary>
        private MainWindow mainWindow;

        //The ShowAnEventWindow Constructor 
        /// <summary>
        /// The ShowAnEventWindow Constructor 
        /// </summary>
        /// <param name="eventDay"> Contains a date selected by the user  </param>
        public ShowAnEventWindow(DateTime eventDay, MainWindow main)
        {
            this.EventDate = eventDay;
            mainWindow = main;
            InitializeComponent();
            SetGrayForeground();
            UpdateGrid();
            CreateEvent(eventDay);
        }

        // The metod displays a selected DataGrid section in other TextBoxs
        /// <summary>
        /// The metod displays a selected DataGrid section in other TextBoxs
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void EventGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.EventGrid.SelectedIndex >= 0)
            {
                if (this.EventGrid.SelectedItems.Count >= 0)
                {
                    var row = this.EventGrid.SelectedItems[0];
                    string arg1 = row.ToString();
                    string arg2 = "Nr = ";
                    string arg3 = ",";
                    string Idstring = Between(arg1, arg2, arg3);
                    updatingEventID = int.Parse(Idstring);
                    DataBaseEventDbContext db = new DataBaseEventDbContext();
                    var r = from d in db.DataBaseEvents1
                            where d.Id == updatingEventID
                            select d;
                    foreach (var item in r)
                    {
                        SetBlackForeground(item.Name, item.Localization, item.StartTime, item.StopTime, item.RemindTime);
                    }
                }
            }
        }

        // The metod delete the information from database
        /// <summary>
        /// The metod delete the information from database
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want Delete?", "Delete Event",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            DataBaseEventDbContext db1 = new DataBaseEventDbContext();
            var r = from d in db1.DataBaseEvents1
                    where d.Id == this.updatingEventID
                    select d;

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                DataBaseEvent1 obj = r.SingleOrDefault();

                if (obj != null)
                {
                    SetGrayForeground();
                    db1.DataBaseEvents1.Remove(obj);
                    db1.SaveChanges();
                }
            }
            UpdateGrid();
            mainWindow.RefreshAllDayButtons();
            mainWindow.ShowCurrentDay();
            mainWindow.AddEventToStackPanel();
        }

        // The metod save change information to the database
        /// <summary>
        /// The metod save change information to the database
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void ButtonSaveChange_Click(object sender, RoutedEventArgs e)
        {
            string input1 = EventStartShow.Text;
            string input2 = EventStopShow.Text;
            TimeSpan time1;
            TimeSpan time2;
            if (TimeSpan.TryParse(input1, out time1) && TimeSpan.TryParse(input2, out time2))
            {
                if (TimeSpan.Compare(time1, time2) < 0)
                {
                    TimeSpan ts = new TimeSpan(RemindCombobox.SelectedIndex, 0, 0);
                    DateTime from_date = new DateTime(EventDate.Year, EventDate.Month, EventDate.Day) + time1;
                    DataBaseEventDbContext db = new DataBaseEventDbContext();
                    var r = from d in db.DataBaseEvents1
                            where d.Id == updatingEventID
                            select d;

                    DataBaseEvent1 obj = r.SingleOrDefault();
                    if (obj != null)
                    {
                        obj.Name = EventNameShow.Text;
                        obj.Localization = EventLocalizationShow.Text;
                        obj.Year = EventDate.Year;
                        obj.Month = EventDate.Month;
                        obj.Day = EventDate.Day;
                        obj.StartTime = EventStartShow.Text;
                        obj.StopTime = EventStopShow.Text;
                        obj.RemindTime = from_date - ts;
                    }
                    db.SaveChanges();
                    UpdateGrid();
                    mainWindow.RefreshAllDayButtons();
                    mainWindow.ShowCurrentDay();
                    mainWindow.AddEventToStackPanel();
                }
                else
                {
                    AlertWindow alertWindow = new AlertWindow("Please, corect your information!", "Input Error");
                    alertWindow.ShowDialog();
                }
            }
            else
            {
                AlertWindow alertWindow = new AlertWindow("Please, corect your information!", "Input Error");
                alertWindow.ShowDialog();
            }
        }

        // The method changes the title of the window
        /// <summary>
        /// The method changes the title of the window
        /// </summary>
        /// <param name="when"> Contains date which was selected by user </param>
        private void CreateEvent(DateTime when)
        {
            WindowTitle.Text = when.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        }

        // The metod close the window after click on button
        /// <summary>
        /// The metod close the window after click on button
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // A method that allows change the position of the window
        /// <summary>
        /// A method that allows change the position of the window
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        // The metod refresh the window after click on button
        /// <summary>
        /// The metod refresh the window after click on button
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
            SetGrayForeground();
        }

        // The method extracts characters between the first and second string from the string
        /// <summary>
        /// The method extracts characters between the first and second string from the string
        /// </summary>
        /// <param name="AllString"> Contains processed string </param>
        /// <param name="FirstString"> Contains the beginning of a string after which we want to look </param>
        /// <param name="LastString"> Contains the ending of a string before which we want to look </param>
        /// <returns> Return string beetwen FirstString and LastString in AllString </returns>
        private string Between(string AllString, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = AllString.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = AllString.IndexOf(LastString);
            FinalString = AllString.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        // The metod change the appearance of the elements to be active
        /// <summary>
        /// The metod change the appearance of the elements to be unactive
        /// </summary>
        private void SetGrayForeground()
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            this.EventNameShow.Foreground = grayBrush;
            this.EventNameShow.Text = "Event name";
            this.EventNameShow.IsReadOnly = true;
            this.EventLocalizationShow.Foreground = grayBrush;
            this.EventLocalizationShow.Text = "Localization";
            this.EventLocalizationShow.IsReadOnly = true;
            this.EventStartShow.Foreground = grayBrush;
            this.EventStartShow.Text = "00:00";
            this.EventStartShow.IsReadOnly = true;
            this.EventStopShow.Foreground = grayBrush;
            this.EventStopShow.Text = "00:00";
            this.EventStopShow.IsReadOnly = true;
            this.RemindCombobox.Foreground = grayBrush;
            this.RemindCombobox.SelectedIndex = 0;
            this.RemindCombobox.IsEnabled = false;
            this.AllDayCheckBox.IsChecked = false;
            this.AllDayCheckBox.IsEnabled = false;
        }

        // The metod change the appearance of the elements to be active
        /// <summary>
        /// The metod change the appearance of the elements to be active
        /// </summary>
        /// <param name="name"> Contains selected event name </param>
        /// <param name="localization"> Contains selected event localization </param>
        /// <param name="start"> Contains selected time to start event </param>
        /// <param name="stop"> Contains selected end time event </param>
        /// <param name="remind"> Contains selected remind date event</param>
        private void SetBlackForeground(string name, string localization, string start, string stop, DateTime remind)
        {
            TimeSpan time;
            TimeSpan.TryParse(start, out time);
            DateTime date = new DateTime(EventDate.Year, EventDate.Month, EventDate.Day) + time;
            int hour = 0;
            if (date.Hour - remind.Hour >= 0)
            {
                hour = date.Hour - remind.Hour;
            }
            else
            {
                hour = date.Hour - remind.Hour + 24;
            }
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            this.EventNameShow.Foreground = blackBrush;
            this.EventNameShow.Text = name;
            this.EventNameShow.IsReadOnly = false;
            this.EventLocalizationShow.Foreground = blackBrush;
            this.EventLocalizationShow.Text = localization;
            this.EventLocalizationShow.IsReadOnly = false;
            this.EventStartShow.Foreground = blackBrush;
            this.EventStartShow.Text = start;
            this.EventStartShow.IsReadOnly = false;
            this.EventStopShow.Foreground = blackBrush;
            this.EventStopShow.Text = stop;
            this.EventStopShow.IsReadOnly = false;
            this.RemindCombobox.Foreground = blackBrush;
            this.RemindCombobox.SelectedIndex = hour;
            this.RemindCombobox.IsEnabled = true;
            this.AllDayCheckBox.IsEnabled = true;
        }

        // The metod takes an id focused DataGrid row and change color of information
        /// <summary>
        /// The metod takes an id focused DataGrid row and change color of information
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void EventGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.EventGrid.SelectedIndex >= 0)
            {
                if (this.EventGrid.SelectedItems.Count >= 0)
                {
                    var row = this.EventGrid.SelectedItems[0];
                    string arg1 = row.ToString();
                    string arg2 = "Nr = ";
                    string arg3 = ",";
                    string Idstring = Between(arg1, arg2, arg3);
                    updatingEventID = int.Parse(Idstring);
                    DataBaseEventDbContext db = new DataBaseEventDbContext();
                    var r = from d in db.DataBaseEvents1
                            where d.Id == updatingEventID
                            select d;
                    foreach (var item in r)
                    {
                        SetBlackForeground(item.Name, item.Localization, item.StartTime, item.StopTime, item.RemindTime);
                    }
                }
            }
        }

        // Method to update the DataGrid
        /// <summary>
        /// Method to update the DataGrid
        /// </summary>
        public void UpdateGrid()
        {
            DataBaseEventDbContext db = new DataBaseEventDbContext();
            var docs = from d in db.DataBaseEvents1
                       where d.Day == EventDate.Day && d.Month == EventDate.Month && d.Year == EventDate.Year
                       select new
                       {
                           Nr = d.Id,
                           Name = d.Name,
                           Localization = d.Localization,
                           Year = d.Year,
                           Month = d.Month,
                           Day = d.Day,
                           From = d.StartTime,
                           To = d.StopTime,
                           Remind = d.RemindTime,
                       };
            this.EventGrid.ItemsSource = docs.ToList();
        }

        // CheckBox_Checked is an event which set default time for all day
        /// <summary>
        /// CheckBox_Checked is an event which set default time for all day
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            this.EventStartShow.Foreground = blackBrush;
            this.EventStopShow.Foreground = blackBrush;
            this.EventStartShow.Text = "00:00";
            this.EventStopShow.Text = "23:59";
            this.EventStartShow.IsReadOnly = true;
            this.EventStopShow.IsReadOnly = true;
        }

        // CheckBox_Checked is an event which set default time for none
        /// <summary>
        /// CheckBox_Checked is an event which set default time for none
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.EventStartShow.Text = "00:00";
            this.EventStopShow.Text = "00:00";
            this.EventStartShow.IsReadOnly = false;
            this.EventStopShow.IsReadOnly = false;
        }
    }
}
