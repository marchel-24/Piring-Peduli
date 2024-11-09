using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;

namespace PiringPeduliWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Get the connection string from App.config
            string connectionString = "host=localhost;port=5432;Database=PiringPeduli;Username=postgres;Password=Nazril0908";

            // Pass the connection string to the UserRepository and UserService
            var sortRepo = new OrderRepository(connectionString);
            var AccountRepo = new CourierRepository(connectionString);
            var AccountService = new CourierService(AccountRepo);
            var sortService = new OrderService(sortRepo);

            try
            {
                //AccountService.CreateCourier("siappaaa", VehicleType.Motorcycle, 4);
                //sortService.PublishSortedWaste(1, SortedType.Others, 0.9);
                sortService.CreateOrder(1, StatusType.Delivered, 1, 3, 1,"Hai hai");
                Debug.WriteLine("Berhasil");
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

}
