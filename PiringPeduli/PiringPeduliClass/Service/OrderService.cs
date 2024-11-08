using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using System;
using System.Collections.Generic;

namespace PiringPeduliClass.Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void CreateOrder(int orderId, StatusType status, int source, int destination, int courier, string description)
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

            _orderRepository.AddOrder(order);
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
    }
}

