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
using ApiLibrary;

namespace CalendarMate
{
    // The CurrentCityWindow containes window mechanics displaying from data base and adding events to data base
    /// <summary>
    /// The <c> CurrentCityWindow </c> class
    /// </summary>
    public partial class CurrentCityWindow : Window
    {
        //Serializer class
        /// <summary>
        /// Serializer class
        /// </summary>
        DataBaseLocalizationSerializer DbLocalization = new DataBaseLocalizationSerializer();

        // MainWindow object to store the reference to our main window
        /// <summary>
        /// MainWindow object to store the reference to our main window.
        /// </summary>
        MainWindow mainWindow;

        // The CurrentCityWindow constructor
        /// <summary>
        /// The CurrentCityWindow constructor. 
        /// </summary>
        public CurrentCityWindow(MainWindow main)
        {
            InitializeComponent();
            EventLocalization.Text = DbLocalization.GetLocalization();
            mainWindow = main;
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

        // The metod save change information to the database
        /// <summary>
        /// The metod save change information to the database
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            CurrentWeatherInfoModel currentWeather = await CurrentWeatherInfoProcessor.LoadCurrentWeather(EventLocalization.Text);
            if (currentWeather.Cod == "404")
            {
                AlertWindow alertWindow = new AlertWindow("No such city! Try Again!", "Input Error");
                alertWindow.ShowDialog();
            }
            else
            {
                DbLocalization.SaveLocalization(EventLocalization.Text);
                mainWindow.LoadCurrentCity();
                mainWindow.LoadCurrentWeather();
            } 
        }
    }
}
