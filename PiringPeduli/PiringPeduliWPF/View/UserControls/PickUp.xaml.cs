using Microsoft.Maps.MapControl.WPF;
using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for PickUp.xaml
    /// </summary>
    public partial class PickUp : UserControl
    {
        public PickUp()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDb"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var orderRepository = new OrderRepository(connectionString);
            var accountRepository = new AccountRepository(connectionString);
            var orderService = new OrderService(orderRepository, accountRepository);
            DataContext = new PickUpViewModel(orderService);

            var apikey = ConfigurationManager.AppSettings["BingMapsApiKey"];
            BingMap.CredentialsProvider = new ApplicationIdCredentialsProvider(apikey);
            DescriptionBox.GotFocus += DescriptionBox_GotFocus;
            DescriptionBox.LostFocus += DescriptionBox_LostFocus;
        }

        private void TxtWeight_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ArrowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            QuestionComboBox.IsDropDownOpen = !QuestionComboBox.IsDropDownOpen;
        }

        private void DescriptionBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DescriptionBox.Text == "Enter your description...")
            {
                DescriptionBox.Text = string.Empty;
                DescriptionBox.Foreground = new SolidColorBrush(Colors.Black);  // Warna teks ketika mulai mengetik
            }
        }

        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                DescriptionBox.Text = "Enter your description...";  // Placeholder
                DescriptionBox.Foreground = new SolidColorBrush(Colors.Gray);  // Warna placeholder
            }
        }

    }
}
