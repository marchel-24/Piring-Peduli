using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;
using PiringPeduliWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace PiringPeduliWPF.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly OrderService _orderService;
        private string _username;
        private string _address;
        private ObservableCollection<Order> _orders;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public AccountViewModel()
        {
        }

        public AccountViewModel(OrderService orderService)
        {
            _orderService = orderService;
            _orders = new ObservableCollection<Order>();
            Username = UserSessionService.Account.Username;
            LoadOrder();
        }

        private void LoadOrder()
        {
            var orders = _orderService.GetOrderById(UserSessionService.Account.AccountId);

            Orders.Clear();

            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
