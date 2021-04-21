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
    /// <summary>
    /// Logika interakcji dla klasy DecisionWindow.xaml
    /// </summary>
    public partial class DecisionWindow : Window
    {
        // The decision
        /// <value>Containes the users decision.</value>
        private bool decision = false;

        // Creates the decision window with given strings
        /// <summary>
        /// Creates the decision window with given strings.
        /// </summary>
        /// <param name="question">String alert containing alert text.</param>
        /// <param name="windowName">String windowName containing window name.</param>
        public DecisionWindow(string question, string windowName)
        {
            InitializeComponent();
            QuestionText.Text = question;
            DecisionWindowName.Text = windowName;
        }

        public bool ShowDialog(bool customShowDialog)
        {
            this.ShowDialog();
            return decision;
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

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            decision = true;
            this.Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            decision = false;
            this.Close();
        }
    }
}
