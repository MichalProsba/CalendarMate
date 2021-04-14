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
    /// <summary>
    /// The window to collect information about even an event from users
    /// </summary>
    public partial class AddAnEventWindow : Window
    {
        private bool ComboSelected = false;
        /// <summary>
        /// the variable that stores information about which date was chosen
        /// </summary>
        private DateTime EventDate;

        /// <summary>
        /// Window constructor
        /// </summary>
        /// <param name="eventDay"></param>
        public AddAnEventWindow(DateTime eventDay)
        {
            this.EventDate = eventDay;
            InitializeComponent();
            CreateEvent(eventDay);
        }

        /// <summary>
        /// ButtonAdd_Click is an event which add new event to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = new TimeSpan(RemindCombobox.SelectedIndex, 0, 0);
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
                RemindTime = EventDate.Date + ts,
            };

            db1.DataBaseEvents1.Add(doctroObject);
            db1.SaveChanges();
            RestartWindow();
        }

        /// <summary>
        /// ButtonShow_Click is an event which show new event list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            ShowAnEventWindow oneList = new ShowAnEventWindow(EventDate.Date);
            oneList.Show();
        }

        /// <summary>
        /// TextBox_GotFocus is an event which changes the appearance of focused textboxes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// TextBox_LostFocus is an event which changes the appearance of lost focused textboxes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// CheckBox_Checked is an event which set default
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void CreateEvent(DateTime when)
        {
            WindowTitle.Text = when.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        }

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
