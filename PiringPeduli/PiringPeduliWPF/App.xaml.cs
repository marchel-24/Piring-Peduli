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
            string connectionString = "host=localhost;port=5432;Database=postgres;Username=postgres;Password=lol12345";

            // Pass the connection string to the UserRepository and UserService
            var sortRepo = new SortedWasteRepository(connectionString);
            var AccountRepo = new AccountRepository(connectionString);
            var TempoRepo = new TemporaryStorageRepository(connectionString);
            var TempoServ = new TemporaryStorageService(TempoRepo);
            var AccountService = new AccountService(AccountRepo);
            var sortService = new SortedWasteService(sortRepo);
            var wasteInStorageRepo = new WasteInStorageRepository(connectionString);
            var wasteInStorageService = new WasteInStorageService(wasteInStorageRepo);
            var requiredWasteRepository = new RequiredWasteRepository(connectionString);
            var requiredWasteService = new RequiredWasteService(requiredWasteRepository);



            try
            {
                //AccountService.CreateCourier("siappaaa", VehicleType.Motorcycle, 4);
                //sortService.PublishSortedWaste(SortedType.Others);
                AccountService.RemoveAccountByUsername("ilham");
                //TempoServ.CreateTemporaryStorage("Jalan Roma", 1);
                //wasteInStorageService.DeleteWasteByStorage(1);
                //requiredWasteService.AddRequiredWaste(1, 2);

                //Debug.WriteLine(AccountService.GetRecyclerByName("ilham").RecyclerId);
                //Debug.WriteLine(AccountService.GetRecyclerByName("ilham").RecyclerName);
                //Debug.WriteLine(AccountService.GetRecyclerByName("ilham").RecyclerAddress);
                //Debug.WriteLine(AccountService.GetRecyclerByName("ilham").AccountId);
                //sortService.CreateOrder(1, StatusType.Delivered, 1, 3, 1,"Hai hai");
                Debug.WriteLine("Berhasil");
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

}
