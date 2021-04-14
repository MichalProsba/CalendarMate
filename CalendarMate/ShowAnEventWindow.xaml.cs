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
    /// Logika interakcji dla klasy ShowAnEventWindow.xaml
    /// </summary>
    public partial class ShowAnEventWindow : Window
    {
        /// <summary>
        /// variable dataGridSelcted holds the information about that DataGrid was selected
        /// </summary>
        private bool dataGridSelcted = false;
        /// <summary>
        /// variable updatingEventID holds the current id
        /// </summary>
        private int updatingEventID = 0;
        /// <summary>
        /// variable updatingEventID holds the current date
        /// </summary>
        private DateTime eventDate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventDay"></param>
        public ShowAnEventWindow(DateTime eventDay)
        {
            this.eventDate = eventDay;
            InitializeComponent();
            SetGrayForeground();
            UpdateGrid();
            CreateEvent(eventDay);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        SetBlackForeground(item.Name, item.Localization, item.StartTime, item.StopTime);
                    }
                }
            }
        }

        /// <summary>
        /// Delete Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }

        /// <summary>
        /// Add Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveChange_Click(object sender, RoutedEventArgs e)
        {
            DataBaseEventDbContext db = new DataBaseEventDbContext();
            var r = from d in db.DataBaseEvents1
                    where d.Id == updatingEventID
                    select d;

            DataBaseEvent1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Name = EventNameShow.Text;
                obj.Localization = EventLocalizationShow.Text;
                obj.Year = eventDate.Year;
                obj.Month = eventDate.Month;
                obj.Day = eventDate.Day;
                obj.StartTime = EventStartShow.Text;
                obj.StopTime = EventStopShow.Text;
            }
            db.SaveChanges();
            UpdateGrid();
        }

        private void CreateEvent(DateTime when)
        {
            WindowTitle.Text = when.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
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

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
            SetGrayForeground();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="STR"></param>
        /// <param name="FirstString"></param>
        /// <param name="LastString"></param>
        /// <returns></returns>
        private string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

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
        }

        private void SetBlackForeground(string name, string localization, string start, string stop)
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
        }

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
                        SetBlackForeground(item.Name, item.Localization, item.StartTime, item.StopTime);
                    }
                }
            }
        }

        private void UpdateGrid()
        {
            DataBaseEventDbContext db = new DataBaseEventDbContext();
            var docs = from d in db.DataBaseEvents1
                       where d.Day == eventDate.Day && d.Month == eventDate.Month && d.Year == eventDate.Year
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

    }
}
