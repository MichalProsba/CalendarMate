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
    /// Logika interakcji dla klasy AddAnEventWindow2.xaml
    /// </summary>
    public partial class AddAnEventWindow : Window
    {
        private DateTime EventDate;
        public AddAnEventWindow(DateTime eventDay)
        {
            this.EventDate = eventDay;
            InitializeComponent();
            EventDbContext db = new EventDbContext();
            var docs = from d in db.UserEvents
                       where d.Day == EventDate.Day
                       select new
                       {
                           Name = d.Name,
                           Localization = d.Localization,
                           Year = d.Year,
                           Month = d.Month,
                           Day = d.Day,
                           From = d.StartTime.ToShortTimeString(),
                           To = d.StartTime.ToShortTimeString(),
                       };
            CreateEvent(eventDay);
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

        /// <summary>
        /// Add Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EventDbContext db1 = new EventDbContext();
            UserEvent doctroObject = new UserEvent()
            {
                Name = EventName.Text,
                Localization = EventLocalization.Text,
                Year = EventDate.Year,
                Month = EventDate.Month,
                Day = EventDate.Day,
                StartTime = DateTime.Parse(EventStart.Text),
                StopTime = DateTime.Parse(EventStop.Text),
            };
            db1.UserEvents.Add(doctroObject);
            db1.SaveChanges();
            EventDbContext db = new EventDbContext();
        }


        /// <summary>
        /// Delete Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            ShowAnEventWindow oneList = new ShowAnEventWindow(EventDate.Date);
            oneList.Show();
        }
    }
}
