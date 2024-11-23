using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PiringPeduliClass.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly AccountRepository _accountRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderService(OrderRepository orderRepository, AccountRepository accountRepository)
        {
            _orderRepository = orderRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> CreateOrderCustomerAsync(StatusType status, Account source, string description, Size size)
        {
            try
            {
                // Get all temporary storage accounts asynchronously
                List<Account> temporaryStorageAccounts = await _accountRepository.GetAllAccountsTemporaryStorageAsync();

                // Find the nearest account based on source location
                var destinationAccount = GeoService.FindNearestAccount(source.Lat, source.Lon, temporaryStorageAccounts);

                // Create a new order object
                var order = new Order
                {
                    Status = status,
                    Source = source.AccountId,
                    Destination = destinationAccount.AccountId,
                    Description = description,
                    Size = size
                };

                // Add the order asynchronously
                bool isOrderAdded = await _orderRepository.AddOrderAsync(order);

                if (!isOrderAdded)
                {
                    throw new Exception("Failed to create order.");
                }

                return true; // Order created successfully
            }
            catch (Exception ex)
            {
                // Log the exception if necessary (or replace with your logging system)
                Debug.WriteLine($"Error creating order: {ex.Message}");

                return false; // Return false if an error occurs
            }
        }


        public void UpdateOrder(int orderId, StatusType status, int source, int destination, int courier, string description)
        {
            var order = new Order
            {
                OrderId = orderId,
                Status = status,
                Source = source,
                Destination = destination,
                Courier = courier,
                Description = description
            };

            _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int orderId)
        {
            _orderRepository.DeleteOrder(orderId);
        }

        public List<Order> GetOrderById(int sourceorderId)
        {
            List<Order> orders = _orderRepository.GetOrdersBySourceId(sourceorderId);
            return orders;
        }
    }
}

