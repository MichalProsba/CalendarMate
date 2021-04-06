using Event.Domain.Models;
using Event.EntityFramework;
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

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
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
            this.gridDoctors.ItemsSource = docs.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            EventDbContext db = new EventDbContext();
            UserEvent doctroObject = new UserEvent()
            {
                Name = txtName.Text,
                Localization = txtSpecialization.Text,
                //Date = DateTime.Parse(txtQualification.Text)

            };
            db.UserEvents.Add(doctroObject);
            db.SaveChanges();
        }
        private void BtnLoadDoctors_Click(object sender, RoutedEventArgs e)
        {
            EventDbContext db = new EventDbContext();
            this.gridDoctors.ItemsSource = db.UserEvents.ToList();
        }

        private void BtnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            EventDbContext db = new EventDbContext();
            var r = from d in db.UserEvents
                    where d.Id == this.updatingDoctorID
                    select d;

            UserEvent obj = r.SingleOrDefault();

            if (obj != null)
            {
                obj.Name = this.txtName2.Text;
                obj.Localization = this.txtSpecialization2.Text;
                //obj.Date = DateTime.Parse(this.txtQualification2.Text);
            }

            db.SaveChanges();
        }

        private int updatingDoctorID = 0;

        private void gridDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridDoctors.SelectedIndex >= 0)
            {
                if (this.gridDoctors.SelectedItems.Count >= 0)
                {
                    if (this.gridDoctors.SelectedItems[0].GetType() == typeof(UserEvent))
                    {
                        UserEvent d = (UserEvent)this.gridDoctors.SelectedItems[0];
                        this.txtName2.Text = d.Name;
                        this.txtSpecialization2.Text = d.Localization;
                        //this.txtQualification2.Text = d.Date.ToString();
                        this.updatingDoctorID = d.Id;
                    }
                }
            }
        }

        private void BtnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want Delete?", "Delete Doctor",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);

            EventDbContext db = new EventDbContext();
            var r = from d in db.UserEvents
                    where d.Id == this.updatingDoctorID
                    select d;

            if (msgBoxResult == MessageBoxResult.Yes)
            {
                UserEvent obj = r.SingleOrDefault();

                if (obj != null)
                {
                    db.UserEvents.Remove(obj);
                    db.SaveChanges();
                }
            }
        }




    }
}
