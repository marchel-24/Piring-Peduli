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
        private string _username;
        private string _address;
        private string _accounttype;
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


        public string accountType
        {
            get => _accounttype;
            set
            {
                _accounttype = value;
                OnPropertyChanged(nameof(AccountType));
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
            _orders = new ObservableCollection<Order>();
            Username = UserSessionService.Account.Username;
            accountType = UserSessionService.Account.Type.ToString();
            LoadOrder();
        }


        private void LoadOrder()
        {
            var orders = DatabaseService.orderService.GetOrderById(UserSessionService.Account.AccountId);

            Orders.Clear();

            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
    }
}
