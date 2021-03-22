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
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(DateTime eventDay)
        {
            InitializeComponent();
            CreateEvent(eventDay);
        }

        private void CreateEvent(DateTime when)
        {
            Event_Date.Text = when.ToString("D", CultureInfo.CreateSpecificCulture("en-US"));
        }

        private void CloseEventWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MoveEventWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
