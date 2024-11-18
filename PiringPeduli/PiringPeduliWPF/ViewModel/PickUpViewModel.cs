using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
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

        public PickUpViewModel()
        {
            OrderCommand = new ViewModeCommand(Order);
        }

        public PickUpViewModel(OrderService orderService) 
        {
            _orderService = orderService;

            OrderCommand = new ViewModeCommand(Order);
        }

        public ICommand OrderCommand { get; }

        private async void Order(object obj)
        {
            try
            {
                // Attempt to create the order asynchronously
                bool isOrderCreated = await _orderService.CreateOrderCustomerAsync(StatusType.Processing, UserSessionService.Account, "TES2");

                if (!isOrderCreated)
                {
                    throw new Exception("Failed to create order.");
                }

                // Optionally, provide feedback to the user upon success
                MessageBox.Show("Order created successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Handle any errors and provide feedback to the user
                MessageBox.Show($"An error occurred: {ex.Message}", "Order Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
