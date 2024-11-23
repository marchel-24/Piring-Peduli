using Microsoft.Maps.MapControl.WPF;
using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.View.Component;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections;
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
    /// Interaction logic for PickUpCourier.xaml
    /// </summary>
    public partial class PickUpCourier : UserControl
    {
        public PickUpCourier()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDb"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var orderRepository = new OrderRepository(connectionString);
            var orderService = new OrderService(orderRepository);
            DataContext = new PickUpCourierViewModel(orderService);

            var apikey = ConfigurationManager.AppSettings["BingMapsApiKey"];
            BingMap.CredentialsProvider = new ApplicationIdCredentialsProvider(apikey);
            LoadOrder();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadOrder()
        {
            OrderList.Children.Clear(); // Clear existing items

            if(DataContext is PickUpCourierViewModel viewmodel)
            {
                foreach (var order in viewmodel.Orders)
                {
                    var container = new CourierContainer
                    {
                        DataContext = new
                        {
                            Asal = order.SourceAddress,
                            Type = order.Size.ToString(),
                            Deskripsi = order.Description,
                            Tujuan = order.DestinationAddress
                        }
                    };
                    OrderList.Children.Add(container);
                }
            }
        }
    }
}
