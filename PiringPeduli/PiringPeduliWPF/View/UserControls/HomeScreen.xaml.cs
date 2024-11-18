using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

namespace PiringPeduliWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        public HomeScreen()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDb"].ConnectionString;
            var orderRepository = new OrderRepository(connectionString);
            var orderService = new OrderService(orderRepository);
            DataContext = new AccountViewModel(orderService);
        }

        private void OpenWebsiteCNN(object sender, RoutedEventArgs e)
        {
            string url = "https://edition.cnn.com/climate/solutions";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void OpenWebsiteKompas(object sender, RoutedEventArgs e)
        {
            string url = "https://lestari.kompas.com/category/lingkungan";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void OpenWebsiteDetik(object sender, RoutedEventArgs e)
        {
            string url = "https://health.detik.com/";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
