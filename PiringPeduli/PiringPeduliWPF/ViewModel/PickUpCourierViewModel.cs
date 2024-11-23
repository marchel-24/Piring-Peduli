﻿using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliWPF.ViewModel
{
    public class PickUpCourierViewModel: ViewModelBase
    {
        private readonly OrderService _orderService;

        public PickUpCourierViewModel()
        {
        }

        public PickUpCourierViewModel(OrderService orderService)
        {
            _orderService = orderService;
            _orders = new ObservableCollection<Order>();
            LoadOrder();
        }

        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        private void LoadOrder()
        {
            var orders = _orderService.GetOrders();

            Orders.Clear();

            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
