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
                           EventDate = d.Date
                       };
            foreach (var item in docs)
            {
                Console.WriteLine(item.EventName);
                Console.WriteLine(item.EventLocalization);
                Console.WriteLine(item.EventDate);
            }
            this.gridDoctors.ItemsSource = docs.ToList();
        }

        private void BtnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void BtnLoadDoctors_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void BtnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void gridDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("");
        }
    }
}
