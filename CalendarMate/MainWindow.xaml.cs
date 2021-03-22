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
using System.Windows.Threading;

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
            DispatcherTimer actualTime = new DispatcherTimer();
            actualTime.Tick += new EventHandler(UpdateCurrentTime);
            actualTime.Interval = new TimeSpan(0, 0, 1);
            actualTime.Start();

            CalendarDate MyDate = new CalendarDate();
            MyDate.DisplayDate();

            DateTime today = DateTime.Now;
            //DateTime today = new DateTime(2021,4,6);
            int number_of_days = DateTimeDayOfMonth.DaysInMonth(today);
            int day_of_week = DateTimeDayOfMonth.DayOfWeekOfGivenDate(DateTimeDayOfMonth.FirstDayOfMonth(today));
            int column = 0;
            if (day_of_week != 0)
                column = day_of_week;
            else
                column = 7;
            //int column = 1;
            int correction = column-1;
            int row = 1;
            for (int i = 1; i <= number_of_days; i++)
            {
                Button button = new Button();
                button.Name = "Button_" + i.ToString();
                //button.Content = i;
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);
                /////////////////////////
                TextBlock textBlock = new TextBlock();
                textBlock.Name = "TextBlock_" + i.ToString();
                textBlock.Text = i.ToString();
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.Margin = new Thickness(5,0,0,0);
                textBlock.FontSize = 20;
                Grid.SetColumn(textBlock, column);
                Grid.SetRow(textBlock, row);
                /////////////////////////
                if ((i+correction) % 7 == 0)
                {
                    row++;
                    column = 0;
                }
                column++;
                mainGrid.Children.Add(button);
                mainGrid.Children.Add(textBlock);
            }
            
            
            B1.HorizontalAlignment = HorizontalAlignment.Center;
            B1.VerticalAlignment = VerticalAlignment.Center;
        }

        private void UpdateCurrentTime(object sender, EventArgs e)
        {
            B1.Text = DateTime.Now.ToString();
        }


    }




}
