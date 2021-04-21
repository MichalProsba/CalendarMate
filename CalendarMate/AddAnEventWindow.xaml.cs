//using Event.Domain.Models;
//using Event.EntityFramework;
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
    // The AddAnEventWindow class containes window mechanics add events to database
    /// <summary>
    /// The <c> ShowAnEventWindow </c> class
    /// </summary>
    public partial class AddAnEventWindow : Window
    {
        /// <summary>
        /// Contains information about switching combobox
        /// </summary>
        private bool ComboSelected = false;
        /// <summary>
        /// Contains a date selected by the user
        /// </summary>
        private DateTime EventDate;


        private ShowAnEventWindow oneList;
        // The AddAnEventWindow Constructor 
        /// <summary>
        /// The AddAnEventWindow Constructor 
        /// </summary>
        /// <param name="eventDay"> Contains a date selected by the user  </param>
        public AddAnEventWindow(DateTime eventDay)
        {
            this.EventDate = eventDay;
            oneList = new ShowAnEventWindow(EventDate.Date);
            InitializeComponent();
            CreateEvent(eventDay);
            RestartWindow();
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
            string input1 = EventStart.Text;
            string input2 = EventStop.Text;
            TimeSpan time1;
            TimeSpan time2;
            if (TimeSpan.TryParse(input1, out time1) && TimeSpan.TryParse(input2, out time2))
            {
                if (TimeSpan.Compare(time1, time2) < 0 && this.EventName.Foreground.Opacity == blackBrush.Opacity && this.EventStart.Foreground.Opacity == blackBrush.Opacity && this.EventStop.Foreground.Opacity == blackBrush.Opacity && this.EventLocalization.Foreground.Opacity == blackBrush.Opacity)
                {
                    TimeSpan ts = new TimeSpan(RemindCombobox.SelectedIndex, 0, 0);
                    DateTime from_date = new DateTime(EventDate.Year, EventDate.Month, EventDate.Day) + time1;
                    DataBaseEventDbContext db1 = new DataBaseEventDbContext();
                    DataBaseEvent1 doctroObject = new DataBaseEvent1()
                    {
                        Name = EventName.Text,
                        Localization = EventLocalization.Text,
                        Year = EventDate.Year,
                        Month = EventDate.Month,
                        Day = EventDate.Day,
                        StartTime = EventStart.Text,
                        StopTime = EventStop.Text,
                        RemindTime = from_date - ts,
                    };
                    db1.DataBaseEvents1.Add(doctroObject);
                    db1.SaveChanges();
                    RestartWindow();
                    oneList.UpdateGrid();
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

        // The method show new window which event list in selected day
        /// <summary>
        /// The method show new window which event list in selected day
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            oneList.Show();
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
                if (tb.Name == "EventName")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "Event name";
                }
                else if (tb.Name == "EventLocalization")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "Localization";
                }
                else if (tb.Name == "EventStart")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "00:00";
                }
                else if (tb.Name == "EventStop")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "00:00";
                }
            }
        }

        /// <summary>
        /// CheckBox_Checked is an event which set default time for all day
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            this.EventStart.Foreground = blackBrush;
            this.EventStop.Foreground = blackBrush;
            this.EventStart.Text = "00:00";
            this.EventStop.Text = "23:59";
            this.EventStart.IsReadOnly = true;
            this.EventStop.IsReadOnly = true;
        }

        /// <summary>
        /// CheckBox_Checked is an event which set default time for none
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            this.EventStart.Foreground = grayBrush;
            this.EventStop.Foreground = grayBrush;
            this.EventStart.Text = "00:00";
            this.EventStop.Text = "00:00";
            this.EventStart.IsReadOnly = false;
            this.EventStop.IsReadOnly = false;
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

        // The method change combox appearance 
        /// <summary>
        /// The method change combox appearance 
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboSelected)
            {
                SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
                this.RemindCombobox.Foreground = blackBrush;
            }
            else
            {
                ComboSelected = true;
            }
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

        // Method restart window
        /// <summary>
        /// Method restart window
        /// </summary>
        private void RestartWindow()
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            this.EventName.Foreground = grayBrush;
            this.EventLocalization.Foreground = grayBrush;
            this.EventStart.Foreground = grayBrush;
            this.EventStop.Foreground = grayBrush;
            this.RemindCombobox.Foreground = grayBrush;
            this.EventName.Text = "Event name";
            this.EventLocalization.Text = "Localization";
            this.EventStart.Text = "00:00";
            this.EventStop.Text = "00:00";
            this.EventStart.IsReadOnly = false;
            this.EventStop.IsReadOnly = false;
            this.AllDayCheckBox.IsChecked = false;
            ComboSelected = false;
            this.RemindCombobox.SelectedIndex = 0;
        }
    }
}
