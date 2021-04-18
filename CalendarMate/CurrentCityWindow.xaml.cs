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
using System.Windows.Shapes;

namespace CalendarMate
{
    public partial class CurrentCityWindow : Window
    {
        // The CurrentCityWindow constructor
        /// <summary>
        /// The CurrentCityWindow constructor. 
        /// </summary>
        public CurrentCityWindow()
        {
            InitializeComponent();
            DataBaseLocalizationDbContext db = new DataBaseLocalizationDbContext();
            var r = from d in db.DataBaseLocalizations1
                    select d;
            DataBaseLocalization1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                EventLocalization.Text = obj.Localization.ToString();
            }
        }

        // Closes the current window
        /// <summary>
        /// Closes the current window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Alows to drag the current window
        /// <summary>
        /// Alows to drag the current window .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DataBaseLocalizationDbContext db = new DataBaseLocalizationDbContext();
            var r = from d in db.DataBaseLocalizations1
                    where d.Id == 1
                    select d;
            DataBaseLocalization1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Localization = EventLocalization.Text;
            }
            db.SaveChanges();
        }
    }
}
