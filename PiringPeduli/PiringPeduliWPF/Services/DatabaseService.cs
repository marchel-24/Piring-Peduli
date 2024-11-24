using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliWPF.Services
{
    public class DatabaseService
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDb"].ConnectionString;

        private AccountRepository accountRepository;
        private CourierRepository courierRepository;
        private CustomerRepository customerRepository;
        private RecyclerRepository recyclerRepository;
        private TemporaryStorageRepository temporaryStorageRepository;
        private OrderRepository orderRepository;
        private WasteInStorageRepository wasteInStorageRepository;
        private SortedWasteRepository sortedWasteRepository;

        public static AccountService accountService;
        public static CourierService courierService;
        public static CustomerService customerService;
        public static RecyclerService recyclerService;
        public static TemporaryStorageService temporaryStorageService;
        public static OrderService orderService;
        public static WasteService wasteService;

        public DatabaseService() 
        {
            // Pass the connection string to the UserRepository and UserService
            accountRepository = new AccountRepository(connectionString);
            courierRepository = new CourierRepository(connectionString);
            customerRepository = new CustomerRepository(connectionString);
            recyclerRepository = new RecyclerRepository(connectionString);
            temporaryStorageRepository = new TemporaryStorageRepository(connectionString);
            orderRepository = new OrderRepository(connectionString);
            wasteInStorageRepository = new WasteInStorageRepository(connectionString);
            sortedWasteRepository = new SortedWasteRepository(connectionString);
            

            accountService = new AccountService(accountRepository);
            courierService = new CourierService(courierRepository);
            customerService = new CustomerService(customerRepository);
            recyclerService = new RecyclerService(recyclerRepository);
            temporaryStorageService = new TemporaryStorageService(temporaryStorageRepository);
            orderService = new OrderService(orderRepository, accountRepository);
            wasteService = new WasteService(sortedWasteRepository, wasteInStorageRepository);
        }
    }
}
