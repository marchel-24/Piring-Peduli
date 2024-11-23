using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PiringPeduliWPF.ViewModel
{
    public class PickUpViewModel:ViewModelBase
    {
        private readonly OrderService _orderService;
        private readonly TemporaryStorageService _temporaryStorageService;

        private ObservableCollection<TemporaryStorage> _temporaryStorages;
        public ObservableCollection<TemporaryStorage> TemporaryStorages
        {
            get => _temporaryStorages;
            set
            {
                _temporaryStorages = value;
                OnPropertyChanged(nameof(TemporaryStorages));
            }
        }

        public PickUpViewModel()
        {
            OrderCommand = new ViewModeCommand(Order);
        }

        // Constructor for Dependency Injection
        public PickUpViewModel(OrderService orderService, TemporaryStorageService temporaryStorageService)
        {
            _orderService = orderService;
            _temporaryStorageService = temporaryStorageService;

            OrderCommand = new ViewModeCommand(Order);
            LoadTemporaryStorageCommand = new ViewModeCommand(async _ => await LoadTemporaryStorageAsync());

            TemporaryStorages = new ObservableCollection<TemporaryStorage>();

        }


        public ICommand OrderCommand { get; }
        public ICommand LoadTemporaryStorageCommand { get; }

        private async void Order(object obj)
        {
            try
            {
                if (UserSessionService.Account.Lat != 0.0)
                {

                    bool isOrderCreated = await _orderService.CreateOrderCustomerAsync(StatusType.Processing, UserSessionService.Account, "TES2");

                    if (!isOrderCreated)
                    {
                        throw new Exception("Failed to create order.");
                    }

                    // Optionally, provide feedback to the user upon success
                    MessageBox.Show("Order created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }else
                {
                    MessageBox.Show("Update Akun anda terlebih dahulu", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // Attempt to create the order asynchronously
            }
            catch (Exception ex)
            {
                // Handle any errors and provide feedback to the user
                MessageBox.Show($"An error occurred: {ex.Message}", "Order Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }

        private async Task LoadTemporaryStorageAsync()
        {
            try
            {

                var storages = await _temporaryStorageService.GetAllTemporaryStorageAsync();

                TemporaryStorages.Clear();
                foreach (var storage in storages)
                {
                    TemporaryStorages.Add(storage);

                    //Debug.WriteLine(TemporaryStorages[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load temporary storages: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
