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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalendarMate
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Window1 window = new Window1();
            GenerateDayPanel();

        }
        private void GenerateDayPanel()
        {

            CalendarDate MyDate = new CalendarDate();
            MyDate.DisplayDate();

            DateTime today = DateTime.Today;
            int number_of_days = DateTimeDayOfMonth.DaysInMonth(today);
            int column = 1;
            int row = 1;
            for (int i = 1; i < number_of_days+1; i++)
            {
                Button button = new Button();
                button.Name = "Button_" + i.ToString();
                button.Content = i;
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);
                if (i % 7 == 0)
                {
                    row++;
                    column = 0;
                }
                column++;
                mainGrid.Children.Add(button);
            }
            
            B1.Content = today.ToString();
        }


    }




}
