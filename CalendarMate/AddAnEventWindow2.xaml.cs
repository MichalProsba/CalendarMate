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
    public partial class AddAnEventWindow2 : Window
    {
        private DateTime EventDate;
        public AddAnEventWindow2(DateTime eventDay)
        {
            this.EventDate = eventDay;
            InitializeComponent();
            EventDbContext db = new EventDbContext();
            var docs = from d in db.UserEvents
                           //where d.Name.StartsWith("Dr. A")
                       select new
                       {
                           EventName = d.Name,
                           EventLocalization = d.Localization,
                           //EventDate = d.Date
                       };

            foreach (var item in docs)
            {
                Console.WriteLine(item.EventName);
                Console.WriteLine(item.EventLocalization);
                //Console.WriteLine(item.EventDate);
            }
            this.EventGrid.ItemsSource = docs.ToList();



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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
            EventDbContext db2 = new EventDbContext();
            this.EventGrid.ItemsSource = db2.UserEvents.ToList();
        }

        /// <summary>
        /// Selected Event
        /// </summary>
        private int updatingEventID = 0;

        private void EventGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.EventGrid.SelectedIndex >= 0)
            {
                if (this.EventGrid.SelectedItems.Count >= 0)
                {
                    if (this.EventGrid.SelectedItems[0].GetType() == typeof(UserEvent))
                    {
                        UserEvent d = (UserEvent)this.EventGrid.SelectedItems[0];
                        this.EventNameShow.Text = d.Name;
                        this.EventLocalizationShow.Text = d.Localization;
                        this.EventStartShow.Text = d.StartTime.ToShortTimeString();
                        this.EventStopShow.Text = d.StopTime.ToShortTimeString();
                        this.updatingEventID = d.Id;
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
            EventDbContext db2 = new EventDbContext();
            this.EventGrid.ItemsSource = db2.UserEvents.ToList();
        }
    }
}
