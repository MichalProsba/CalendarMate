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
    // Logic of class ShowAllEventsListWindow.xaml
    /// <summary>
    /// Logic of class ShowAllEventsListWindow.xaml.
    /// </summary>
    public partial class ShowAllEventsListWindow : Window
    {
        // The Combo selected
        /// <value> Variable ComboSelected contains information about switching combobox.</value>
        private bool ComboSelected = false;

        // The id number
        /// <value>Variable updatingEventID holds the current id.</value>
        private int updatingEventID = 0;

        // The date selected by the user
        /// <value>Variable EventDate holds selected by the user date.</value>
        private DateTime EventDate;

        //The ShowAnEventWindow Constructor 
        /// <summary>
        /// The ShowAnEventWindow Constructor.
        /// </summary>
        /// <param name="eventDay">Contains a date selected by the user.</param>
        public ShowAllEventsListWindow(DateTime eventDay)
        {
                this.EventDate = eventDay;
                InitializeComponent();
                SetGrayForeground();
                UpdateGrid();
                CreateEvent(eventDay);
            RestartWindow();
        }

        // The metod displays a selected DataGrid section in other TextBoxs
        /// <summary>
        /// The metod displays a selected DataGrid section in other TextBoxs.
        /// </summary>
        /// <param name="sender">Contains a reference to the object that triggered the event.</param>
        /// <param name="e">Contains state information and event data associated with a routed event.</param>
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
                        SetBlackForeground(item.Name, item.Localization, item.StartTime, item.StopTime, item.RemindTime.Hour, item.Year, item.Month, item.Day);
                    }
                }
            }
        }

        // The metod delete the information from database
        /// <summary>
        /// The metod delete the information from database.
        /// </summary>
        /// <param name="sender">Contains a reference to the object that triggered the event.</param>
        /// <param name="e">Contains state information and event data associated with a routed event.</param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want Delete?", "Delete Event", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
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
        }

        // The metod save change information to the database
        /// <summary>
        /// The metod save change information to the database.
        /// </summary>
        /// <param name="sender">Contains a reference to the object that triggered the event.</param>
        /// <param name="e">Contains state information and event data associated with a routed event.</param>
        private void ButtonSaveChange_Click(object sender, RoutedEventArgs e)
        {
            string input1 = EventStartShow.Text;
            string input2 = EventStopShow.Text;
            string inputdate = EventYearShow.Text + "/" + EventMonthShow.Text + "/" + EventDayShow.Text;
            DateTime time1;
            DateTime time2;
            DateTime time3;

            if (DateTime.TryParse(input1, out time1) && DateTime.TryParse(input2, out time2) && DateTime.TryParse(inputdate, out time3))
            {
                if (DateTime.Compare(time1, time2) < 0)
                {
                    TimeSpan ts = new TimeSpan(RemindComboboxShow.SelectedIndex, 0, 0);
                    DateTime date = new DateTime(int.Parse(EventYearShow.Text), int.Parse(EventMonthShow.Text), int.Parse(EventDayShow.Text));
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
                        obj.RemindTime = date.Date + ts;
                        obj.Year = int.Parse(EventYearShow.Text);
                        obj.Month = int.Parse(EventMonthShow.Text);
                        obj.Day = int.Parse(EventDayShow.Text);
                    }
                    db.SaveChanges();
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("Please corect you information?", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please corect you information?", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // The method changes the title of the window
        /// <summary>
        /// The method changes the title of the window.
        /// </summary>
        /// <param name="when">Contains date which was selected by user.</param>
        private void CreateEvent(DateTime when)
        {
            WindowTitle.Text = when.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        }

        // The metod close the window after click on button
        /// <summary>
        /// The metod close the window after click on button.
        /// </summary>
        /// <param name="sender">Contains a reference to the object that triggered the event.</param>
        /// <param name="e">Contains state information and event data associated with a routed event.</param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // A method that allows change the position of the window
        /// <summary>
        /// A method that allows change the position of the window.
        /// </summary>
        /// <param name="sender">Contains a reference to the object that triggered the event.</param>
        /// <param name="e">Contains state information and event data associated with a routed event.</param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        // The metod refresh the window after click on button
        /// <summary>
        /// The metod refresh the window after click on button.
        /// </summary>
        /// <param name="sender">Contains a reference to the object that triggered the event.</param>
        /// <param name="e">Contains state information and event data associated with a routed event.</param>
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
            this.RemindComboboxShow.Foreground = grayBrush;
            this.RemindComboboxShow.SelectedIndex = 0;
            this.AllDayCheckBoxShow.IsChecked = false;
            this.AllDayCheckBoxShow.IsEnabled = false;
            this.EventYearShow.Foreground = grayBrush;
            this.EventYearShow.Text = EventDate.Date.Year.ToString();
            this.EventYearShow.IsReadOnly = true;
            this.EventMonthShow.Foreground = grayBrush;
            this.EventMonthShow.Text = EventDate.Date.Month.ToString();
            this.EventMonthShow.IsReadOnly = true;
            this.EventDayShow.Foreground = grayBrush;
            this.EventDayShow.Text = EventDate.Date.Day.ToString();
            this.EventDayShow.IsReadOnly = true;
        }

        // The metod change the appearance of the elements to be active
        /// <summary>
        /// The metod change the appearance of the elements to be active
        /// </summary>
        /// <param name="name"> Contains selected event name </param>
        /// <param name="localization"> Contains selected event localization </param>
        /// <param name="start"> Contains selected time to start event </param>
        /// <param name="stop"> Contains selected end time event </param>
        /// <param name="remind_hour"> Contains selected remind hour event</param>
        /// <param name="year"> Contains selected year </param>
        /// <param name="month"> Contains selected month </param>
        /// <param name="day"> Contains selected day </param>
        private void SetBlackForeground(string name, string localization, string start, string stop, int remind_hour, int year, int month, int day)
        {
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
            this.RemindComboboxShow.Foreground = blackBrush;
            this.RemindComboboxShow.SelectedIndex = remind_hour;
            this.AllDayCheckBoxShow.IsEnabled = true;
            this.EventYearShow.Foreground = blackBrush;
            this.EventYearShow.Text = year.ToString();
            this.EventYearShow.IsReadOnly = false;
            this.EventMonthShow.Foreground = blackBrush;
            this.EventMonthShow.Text = month.ToString();
            this.EventMonthShow.IsReadOnly = false;
            this.EventDayShow.Foreground = blackBrush;
            this.EventDayShow.Text = day.ToString();
            this.EventDayShow.IsReadOnly = false;
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
                        SetBlackForeground(item.Name, item.Localization, item.StartTime, item.StopTime, item.RemindTime.Hour, item.Year, item.Month, item.Day);
                    }
                }
            }
        }

        // Method to update the DataGrid
        /// <summary>
        /// Method to update the DataGrid
        /// </summary>
        private void UpdateGrid()
        {
            DataBaseEventDbContext db = new DataBaseEventDbContext();
            var docs = from d in db.DataBaseEvents1
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
        private void CheckBox_Checked_Show(object sender, RoutedEventArgs e)
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
        private void CheckBox_Unchecked_Show(object sender, RoutedEventArgs e)
        {
            this.EventStartShow.Text = "00:00";
            this.EventStopShow.Text = "00:00";
            this.EventStartShow.IsReadOnly = false;
            this.EventStopShow.IsReadOnly = false;
        }

        // The metod save information to the database
        /// <summary>
        /// The metod save information to the database
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            DateTime time1;
            DateTime time2;
            DateTime time3;

            if (DateTime.TryParse(EventStartAdd.Text, out time1) && DateTime.TryParse(EventStopAdd.Text, out time2) && DateTime.TryParse(EventYearAdd.Text + "/" + EventMonthAdd.Text + "/" + EventDayAdd.Text, out time3))
            {

                if (DateTime.Compare(time1, time2) < 0 && this.EventNameAdd.Foreground.Opacity == blackBrush.Opacity && this.EventLocalizationAdd.Foreground.Opacity == blackBrush.Opacity && this.EventStartAdd.Foreground.Opacity == blackBrush.Opacity && this.EventStopAdd.Foreground.Opacity == blackBrush.Opacity)
                {
                    TimeSpan ts = new TimeSpan(RemindComboboxAdd.SelectedIndex, 0, 0);
                    DateTime date = new DateTime(int.Parse(EventYearAdd.Text), int.Parse(EventMonthAdd.Text), int.Parse(EventDayAdd.Text));
                    DataBaseEventDbContext db1 = new DataBaseEventDbContext();
                    DataBaseEvent1 doctroObject = new DataBaseEvent1()
                    {
                        Name = EventNameAdd.Text,
                        Localization = EventLocalizationAdd.Text,
                        Year = int.Parse(EventYearAdd.Text),
                        Month = int.Parse(EventMonthAdd.Text),
                        Day = int.Parse(EventDayAdd.Text),
                        StartTime = EventStartAdd.Text,
                        StopTime = EventStopAdd.Text,
                        RemindTime = date + ts,
                    };
                    db1.DataBaseEvents1.Add(doctroObject);
                    db1.SaveChanges();
                    RestartWindow();
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("Please corect you information?", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please corect you information?", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // The method change the appearance of focused textboxes 
        /// <summary>
        /// The method change the appearance of focused textboxes 
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            TextBox tb = (TextBox)sender;
            if (tb.Foreground.Opacity != blackBrush.Opacity)
            {
                if (tb.IsReadOnly != true)
                {
                    tb.Foreground = blackBrush;
                    tb.Text = "";
                }
            }
        }

        // The metod changes the appearance of lost focused textboxes 
        /// <summary>
        /// The metod changes the appearance of lost focused textboxes 
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                if (tb.Name == "EventNameAdd")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "Event name";
                }
                else if (tb.Name == "EventLocalizationAdd")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "Localization";
                }
                else if (tb.Name == "EventStartAdd")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "00:00";
                }
                else if (tb.Name == "EventStopAdd")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "00:00";
                }
                else if (tb.Name == "EventYearAdd")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = EventDate.Date.Year.ToString();
                }
                else if (tb.Name == "EventMonthAdd")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = EventDate.Date.Month.ToString();
                }
                else if (tb.Name == "EventDayAdd")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = EventDate.Date.Day.ToString();
                }
            }
        }

        /// <summary>
        /// CheckBox_Checked is an event which set default time for all day
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Checked_Add(object sender, RoutedEventArgs e)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            this.EventStartAdd.Foreground = blackBrush;
            this.EventStopAdd.Foreground = blackBrush;
            this.EventStartAdd.Text = "00:00";
            this.EventStopAdd.Text = "23:59";
            this.EventStartAdd.IsReadOnly = true;
            this.EventStopAdd.IsReadOnly = true;
        }

        /// <summary>
        /// CheckBox_Checked is an event which set default time for none
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Unchecked_Add(object sender, RoutedEventArgs e)
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            this.EventStartAdd.Foreground = grayBrush;
            this.EventStopAdd.Foreground = grayBrush;
            this.EventStartAdd.Text = "00:00";
            this.EventStopAdd.Text = "00:00";
            this.EventStartAdd.IsReadOnly = false;
            this.EventStopAdd.IsReadOnly = false;
        }

        // The method change combox appearance 
        /// <summary>
        /// The method change combox appearance 
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboSelected)
            {
                SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
                this.RemindComboboxAdd.Foreground = blackBrush;
            }
            else
            {
                ComboSelected = true;
            }
        }

        // Method restart window
        /// <summary>
        /// Method restart window
        /// </summary>
        private void RestartWindow()
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            this.EventNameAdd.Foreground = grayBrush;
            this.EventLocalizationAdd.Foreground = grayBrush;
            this.EventStartAdd.Foreground = grayBrush;
            this.EventStopAdd.Foreground = grayBrush;
            this.RemindComboboxAdd.Foreground = grayBrush;
            this.EventYearAdd.Foreground = grayBrush;
            this.EventMonthAdd.Foreground = grayBrush;
            this.EventDayAdd.Foreground = grayBrush;
            this.EventNameAdd.Text = "Event name";
            this.EventLocalizationAdd.Text = "Localization";
            this.EventStartAdd.Text = "00:00";
            this.EventStopAdd.Text = "00:00";
            this.EventYearAdd.Text = EventDate.Year.ToString();
            this.EventMonthAdd.Text = EventDate.Month.ToString();
            this.EventDayAdd.Text = EventDate.Day.ToString(); ;
            this.EventStartAdd.IsReadOnly = false;
            this.EventStopAdd.IsReadOnly = false;
            this.AllDayCheckBoxAdd.IsChecked = false;
            ComboSelected = false;
            this.RemindComboboxAdd.SelectedIndex = 0;
        }
    }
}

