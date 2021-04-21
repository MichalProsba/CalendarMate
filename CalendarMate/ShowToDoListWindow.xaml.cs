using System;
using System.Collections.Generic;
using System.Linq;
using DataBaseEvent.Domain.Models;
using DataBaseLocalization.EntityFramework;
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
using DataBaseToDoList.EntityFramework;

namespace CalendarMate
{
    // The ShowToDoListWindowclass containes window mechanics displaying from data base and adding events to data base
    /// <summary>
    /// The <c> ShowToDoListWindow </c> class
    /// </summary>
    public partial class ShowToDoListWindow : Window
    {
        // The id number
        /// <value> Variable updatingEventID holds the current id </value>
        private int updatingToDoListID = 0;

        // The id information which user will add to database
        /// <value> Variable done holds the information id which user will add to database </value>
        private bool done = false;

        //The ShowToDoListWindow Constructor 
        /// <summary>
        /// The ShowToDoListWindowConstructor 
        /// </summary>
        public ShowToDoListWindow()
        {
            InitializeComponent();
            RestartWindow();
            SetGrayForeground();
            UpdateGrid();
        }

        // The metod save information to the database
        /// <summary>
        /// The metod save information to the database
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            if (this.ToDoListNameShow.Foreground.Opacity == blackBrush.Opacity)
            {
                DataBaseToDoListDbContext db1 = new DataBaseToDoListDbContext();
                DataBaseToDoList1 doctroObject = new DataBaseToDoList1()
                {
                    Name = this.ToDoListNameShow.Text,
                    Done = false,
                };
                db1.DataBaseToDoLists1.Add(doctroObject);
                db1.SaveChanges();
                RestartWindow();
                UpdateGrid();
            }
            else
            {
                MessageBox.Show("Please corect you information?", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // The metod delete the information from database
        /// <summary>
        /// The metod delete the information from database
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want Delete?", "Delete Event",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning,
            MessageBoxResult.No);

            DataBaseToDoListDbContext db1 = new DataBaseToDoListDbContext();
            var r = from d in db1.DataBaseToDoLists1
                    where d.Id == this.updatingToDoListID
                    select d;

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                DataBaseToDoList1 obj = r.SingleOrDefault();

                if (obj != null)
                {
                    SetGrayForeground();
                    db1.DataBaseToDoLists1.Remove(obj);
                    db1.SaveChanges();
                }
            }
            UpdateGrid();
        }

        // The metod refresh the window after click on button
        /// <summary>
        /// The metod refresh the window after click on button
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
            RestartWindow();
            SetGrayForeground();
        }

        // The metod save change information to the database
        /// <summary>
        /// The metod save change information to the database
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            DataBaseToDoListDbContext db = new DataBaseToDoListDbContext();
            var r = from d in db.DataBaseToDoLists1
                    where d.Id == updatingToDoListID
                    select d;

            DataBaseToDoList1 obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Name = this.ToDoListEditShow.Text;
                obj.Done = done;
            }
            db.SaveChanges();
            RestartWindow();
            UpdateGrid();
            SetGrayForeground();
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

        // The metod displays a selected DataGrid section in other TextBoxs
        /// <summary>
        /// The metod displays a selected DataGrid section in other TextBoxs
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void EventGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.EventGrid.SelectedIndex >= 0)
            {
                if (this.EventGrid.SelectedItems.Count >= 0)
                {
                    var row = this.EventGrid.SelectedItems[0];
                    string arg1 = row.ToString();
                    string arg2 = "Nr = ";
                    string arg3 = ",";
                    string Idstring = Between(arg1, arg2, arg3);
                    updatingToDoListID = int.Parse(Idstring);
                    DataBaseToDoListDbContext db = new DataBaseToDoListDbContext();
                    var r = from d in db.DataBaseToDoLists1
                            where d.Id == updatingToDoListID
                            select d;
                    foreach (var item in r)
                    {
                        SetBlackForeground(item.Name, item.Done);
                    }
                }
            }
        }


        // CheckBox_Checked is an event which set default time for all day
        /// <summary>
        /// CheckBox_Checked is an event which set default time for all day
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            done = true;
        }

        // CheckBox_Checked is an event which set default time for none
        /// <summary>
        /// CheckBox_Checked is an event which set default time for none
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            done = false;
        }

        // The metod takes an id focused DataGrid row and change color of information
        /// <summary>
        /// The metod takes an id focused DataGrid row and change color of information
        /// </summary>
        /// <param name="sender"> Contains a reference to the object that triggered the event </param>
        /// <param name="e"> Contains state information and event data associated with a routed event  </param>
        private void EventGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.EventGrid.SelectedIndex >= 0)
            {
                if (this.EventGrid.SelectedItems.Count >= 0)
                {
                    var row = this.EventGrid.SelectedItems[0];
                    string arg1 = row.ToString();
                    string arg2 = "Nr = ";
                    string arg3 = ",";
                    string Idstring = Between(arg1, arg2, arg3);
                    updatingToDoListID = int.Parse(Idstring);
                    DataBaseToDoListDbContext db = new DataBaseToDoListDbContext();
                    var r = from d in db.DataBaseToDoLists1
                            where d.Id == updatingToDoListID
                            select d;
                    foreach (var item in r)
                    {
                        SetBlackForeground(item.Name, item.Done);
                    }
                }
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
            if (tb.Text == "")
            {
                if (tb.Name == "ToDoListNameShow")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "Task";
                }
                else if (tb.Name == "ToDoListEditShow")
                {
                    SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
                    grayBrush.Opacity = 0.4;
                    tb.Foreground = grayBrush;
                    tb.Text = "Edit task";
                }
            }

        }

        // Method restart window
        /// <summary>
        /// Method restart window
        /// </summary>
        private void RestartWindow()
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            this.ToDoListNameShow.Foreground = grayBrush;
            this.ToDoListEditShow.Foreground = grayBrush;
            this.ToDoListNameShow.Text = "Task";
            this.ToDoListEditShow.Text = "Edit Task";
            this.ToDoListNameShow.IsReadOnly = false;
            this.ToDoListEditShow.IsReadOnly = false;
            done = false;
        }

        // Method to update the DataGrid
        /// <summary>
        /// Method to update the DataGrid
        /// </summary>
        private void UpdateGrid()
        {
            DataBaseToDoListDbContext db = new DataBaseToDoListDbContext();
            var docs = from d in db.DataBaseToDoLists1
                       select new
                       {
                           Nr = d.Id,
                           Name = d.Name,
                           Done = d.Done,
                       };
            this.EventGrid.ItemsSource = docs.ToList();
        }

        // The method extracts characters between the first and second string from the string
        /// <summary>
        /// The method extracts characters between the first and second string from the string
        /// </summary>
        /// <param name="AllString"> Contains processed string </param>
        /// <param name="FirstString"> Contains the beginning of a string after which we want to look </param>
        /// <param name="LastString"> Contains the ending of a string before which we want to look </param>
        /// <returns> Return string beetwen FirstString and LastString in AllString </returns>
        private string Between(string AllString, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = AllString.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = AllString.IndexOf(LastString);
            FinalString = AllString.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        // The metod change the appearance of the elements to be active
        /// <summary>
        /// The metod change the appearance of the elements to be unactive
        /// </summary>
        private void SetGrayForeground()
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Colors.Gray);
            grayBrush.Opacity = 0.4;
            this.ToDoListEditShow.Foreground = grayBrush;
            this.ToDoListEditShow.Text = "Edit task";
            this.ToDoListEditShow.IsReadOnly = true;
            this.DoneCheckBox.IsChecked = false;
            this.DoneCheckBox.IsEnabled = false;
            done = false;
        }

        // The metod change the appearance of the elements to be active
        /// <summary>
        /// The metod change the appearance of the elements to be active
        /// </summary>
        /// <param name="name"> Contains selected event name </param>
        /// <param name="done"> Contains selected done information </param>
        private void SetBlackForeground(string name, bool done)
        {
            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
            blackBrush.Opacity = 0.9;
            this.ToDoListEditShow.Foreground = blackBrush;
            this.ToDoListEditShow.Text = name;
            this.ToDoListEditShow.IsReadOnly = false;
            this.DoneCheckBox.IsChecked = done;
            this.DoneCheckBox.IsEnabled = true;
        }
    }
}
