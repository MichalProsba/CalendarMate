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
using System.Globalization;

namespace CalendarMate
{
    public partial class MainWindow : Window
    {
        private DateTime current_date;
        public MainWindow()
        {
            InitializeComponent();
            GenerateCurrentTime();
            GenerateDayPanel(DateTime.Now);
            //Window1 window = new Window1();


        }
        private void GenerateDayPanel(DateTime when)
        {
            //CalendarDate MyDate = new CalendarDate();
            //MyDate.DisplayDate();
            current_date = when;
            Month_And_Year_TextBlock.Text = when.ToString("Y", CultureInfo.CreateSpecificCulture("en-US"));
            DateTime today = when;
            //DateTime today = new DateTime(2021,4,6);
            int number_of_days = DateTimeDayOfMonth.DaysInMonth(today);
            int day_of_week = DateTimeDayOfMonth.DayOfWeekOfGivenDate(DateTimeDayOfMonth.FirstDayOfMonth(today));
            int column = 0;
            if (day_of_week != 0)
                column = day_of_week;
            else
                column = 7;
            //int column = 1;
            int correction = column - 1;
            int row = 3;
            for (int i = 1; i <= number_of_days; i++)
            {
                Button button = new Button();
                button.Name = "Button_" + i.ToString();
                button.Click += new RoutedEventHandler(Day_Click);
                button.Background = Brushes.DarkBlue;
                Grid.SetColumn(button, column);
                Grid.SetRow(button, row);
                TextBlock textBlock = new TextBlock();
                textBlock.Name = "TextBlock_" + i.ToString();
                textBlock.Text = i.ToString();
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.Margin = new Thickness(5, 0, 0, 0);
                textBlock.FontSize = 20;
                textBlock.Foreground = Brushes.White;
                Grid.SetColumn(textBlock, column);
                Grid.SetRow(textBlock, row);
                if ((i + correction) % 7 == 0)
                {
                    row++;
                    column = 0;
                }
                column++;
                mainGrid.Children.Add(button);
                mainGrid.Children.Add(textBlock);
            }
        }

        private void GenerateCurrentTime()
        {
            DispatcherTimer actualTime = new DispatcherTimer();
            actualTime.Tick += new EventHandler(UpdateCurrentTime);
            actualTime.Interval = new TimeSpan(0, 0, 1);
            actualTime.Start();
        }
        private void UpdateCurrentTime(object sender, EventArgs e)
        {
            Clock.Content = DateTime.Now.ToString();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            GenerateDayPanel(DateTimeDayOfMonth.NextMonth(current_date));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            GenerateDayPanel(DateTimeDayOfMonth.PreviousMonth(current_date));
        }
        
        private void CurrentDate_Click(object sender, RoutedEventArgs e)
        {
            GenerateDayPanel(DateTime.Now);
        }

        private void Day_Click(object sender, RoutedEventArgs e)
        {
            Window1 oneDay = new Window1(current_date);
            oneDay.Show();
        }
        private void CloseMainWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
