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

        // TextBox_GotFocus is an event which changes the appearance of focused textboxes 
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

        // TextBox_LostFocus is an event which changes the appearance of lost focused textboxes 
        /// <summary>
        /// TextBox_LostFocus is an event which changes the appearance of lost focused textboxes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            tb.Foreground = grayBrush;
            tb.Text = "Localization";
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DataBaseLocalizationDbContext db1 = new DataBaseLocalizationDbContext();
            DataBaseLocalization1 doctroObject = new DataBaseLocalization1()
            {
                Localization = EventLocalization.Text,
            };
            db1.DataBaseLocalizations1.Add(doctroObject);
            db1.SaveChanges();



            //DataBaseLocalizationDbContext db = new DataBaseLocalizationDbContext();
            //var docs = from d in db.DataBaseLocalizations1
            //           select new
            //           {
            //               Localization = d.Localization
            //           };
            //EventLocalization.Text = Localization



            //RestartWindow();
        }
    }
}
