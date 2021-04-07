using Event.Domain.Models;
using Event.EntityFramework;
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
        /// variable updatingEventID holds the current id
        /// </summary>
        private int updatingEventID = 0;
        /// <summary>
        /// variable updatingEventID holds the current date
        /// </summary>
        private DateTime EventDate;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventDay"></param>
        public ShowAnEventWindow(DateTime eventDay)
        {
            this.EventDate = eventDay;
            InitializeComponent();
            EventDbContext db = new EventDbContext();
            var docs = from d in db.UserEvents
                       where d.Day == EventDate.Day
                       select new
                       {
                           Nr = d.Id,
                           Name = d.Name,
                           Localization = d.Localization,
                           Year = d.Year,
                           Month = d.Month,
                           Day = d.Day,
                           From = d.StartTime.ToShortTimeString(),
                           To = d.StartTime.ToShortTimeString(),
                       };
            this.EventGrid.ItemsSource = docs.ToList();
            CreateEvent(eventDay);
        }
        //
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
                    EventDbContext db = new EventDbContext();
                    var r = from d in db.UserEvents
                            where d.Id == updatingEventID
                            select d;
                    foreach (var item in r)
                    {
                        this.EventNameShow.Text = item.Name;
                        this.EventLocalizationShow.Text = item.Localization;
                        this.EventStartShow.Text = item.StartTime.ToShortTimeString();
                        this.EventStopShow.Text = item.StopTime.ToShortTimeString();
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

            EventDbContext db1 = new EventDbContext();
            var r = from d in db1.UserEvents
                    where d.Id == this.updatingEventID
                    select d;

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                UserEvent obj = r.SingleOrDefault();

                if (obj != null)
                {
                    db1.UserEvents.Remove(obj);
                    db1.SaveChanges();
                }
            }

            EventDbContext db = new EventDbContext();
            var docs = from d in db.UserEvents
                       where d.Day == EventDate.Day
                       select new
                       {
                           Nr = d.Id,
                           Name = d.Name,
                           Localization = d.Localization,
                           Year = d.Year,
                           Month = d.Month,
                           Day = d.Day,
                           From = d.StartTime.ToShortTimeString(),
                           To = d.StartTime.ToShortTimeString(),
                       };
            this.EventGrid.ItemsSource = docs.ToList();
        }

        /// <summary>
        /// Add Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveChange_Click(object sender, RoutedEventArgs e)
        {
            EventDbContext db = new EventDbContext();
            var r = from d in db.UserEvents
                    where d.Id == updatingEventID
                    select d;

            UserEvent obj = r.SingleOrDefault();
            if(obj != null)
            {
                obj.Name = EventNameShow.Text;
                obj.Localization = EventLocalizationShow.Text;
                obj.Year = EventDate.Year;
                obj.Month = EventDate.Month;
                obj.Day = EventDate.Day;
                obj.StartTime = DateTime.Parse(EventStartShow.Text);
                obj.StopTime = DateTime.Parse(EventStopShow.Text);
            }
            db.SaveChanges();
            EventDbContext db1 = new EventDbContext();
            var docs1 = from d in db1.UserEvents
                       where d.Day == EventDate.Day
                       select new
                       {
                           Nr = d.Id,
                           Name = d.Name,
                           Localization = d.Localization,
                           Year = d.Year,
                           Month = d.Month,
                           Day = d.Day,
                           From = d.StartTime.ToShortTimeString(),
                           To = d.StartTime.ToShortTimeString(),
                       };
            this.EventGrid.ItemsSource = docs1.ToList();
        }

        private void CreateEvent(DateTime when)
        {
            WindowTitle.Text = when.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            EventDbContext db = new EventDbContext();
            var docs = from d in db.UserEvents
                       where d.Day == EventDate.Day
                       select new
                       {
                           Nr = d.Id,
                           Name = d.Name,
                           Localization = d.Localization,
                           Year = d.Year,
                           Month = d.Month,
                           Day = d.Day,
                           From = d.StartTime.ToShortTimeString(),
                           To = d.StartTime.ToShortTimeString(),
                       };
            this.EventGrid.ItemsSource = docs.ToList();
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
    }
}
