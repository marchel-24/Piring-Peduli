using Microsoft.Maps.MapControl.WPF;
using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;
using PiringPeduliWPF.View.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PiringPeduliWPF.ViewModel
{
    public class PickUpCourierViewModel: ViewModelBase
    {
        public PickUpCourierViewModel()
        {
            _orders = new ObservableCollection<Order>();
            PickUpCommand = new ViewModeCommand(PickUp);
            LoadSourceDestinationCommand = new ViewModeCommand(LoadSourceDestination);
            LoadOrder();
        }

        public PickUpCourierViewModel(PickUpCourier userControl)
        {
            _orders = new ObservableCollection<Order>();
            _pickUpCourier = userControl;
            PickUpCommand = new ViewModeCommand(PickUp);
            LoadSourceDestinationCommand = new ViewModeCommand(LoadSourceDestination);
            LoadOrder();
        }

        private ObservableCollection<Order> _orders;
        private Order? _selectedOrder = null;
        private string _headerTitle;
        private Account? _sourceAccount = null;
        private Account? _destinationAccount = null;
        private PickUpCourier _pickUpCourier;
        private string _btnTxt;


        public ICommand PickUpCommand { get; }
        public ICommand LoadSourceDestinationCommand { get; }

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        public string HeaderTitle
        {
            get => _headerTitle;
            set
            {
                _headerTitle = value;
                OnPropertyChanged(nameof(HeaderTitle));
            }
        }

        public Account? SourceAccount
        {
            get => _sourceAccount;
            set
            {
                _sourceAccount = value;
                OnPropertyChanged(nameof(SourceAccount));
            }
        }

        public Account? DestinationAccount
        {
            get => _destinationAccount;
            set
            {
                _destinationAccount = value;
                OnPropertyChanged(nameof(DestinationAccount));
            }
        }

        public string BtnTxt
        {
            get => _btnTxt;
            set
            {
                _btnTxt = value;
                OnPropertyChanged(nameof(BtnTxt));
            }
        }

        private void LoadOrder()
        {
            Orders.Clear();
            var orders = DatabaseService.orderService.GetOrdersStatusProcessing();
            SelectedOrder = DatabaseService.orderService.GetOrderByCourierId(UserSessionService.Account.AccountId);

            if(SelectedOrder == null)
            {
                HeaderTitle = "Available Orders";
                BtnTxt = "Pick Up";
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }

            }
            else
            {
                HeaderTitle = "Current Order";
                BtnTxt = "Finish";
                Orders.Add(SelectedOrder);
            }
        }

        private void PickUp(object obj)
        {
            if (obj is Order order)
            {
                if(SelectedOrder == null)
                {
                    order.Status = StatusType.Confirmed;
                    order.Courier = UserSessionService.Account.AccountId;
                    SelectedOrder = order; // Update the selected order
                    DatabaseService.orderService.UpdateOrder(order);
                    LoadOrder();
                    LoadSourceDestinationCommand.Execute(null);
                    _pickUpCourier.InitializeMapPinsAsync();
                }
                else
                {
                    order.Status = StatusType.Delivered;
                    order.Courier = UserSessionService.Account.AccountId;
                    SelectedOrder = null;
                    DatabaseService.orderService.UpdateOrder(order);
                    LoadOrder();
                    SourceAccount = null;
                    DestinationAccount = null;
                    _pickUpCourier.InitializeMapPinsAsync();
                }
            }
        }

        private void LoadSourceDestination(object obj)
        {
            try
            {
                if(SelectedOrder != null)
                {
                    SourceAccount = DatabaseService.accountService.GetUserById(SelectedOrder.Source);
                    DestinationAccount = DatabaseService.accountService.GetUserById(SelectedOrder.Destination);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load accounts locations: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
