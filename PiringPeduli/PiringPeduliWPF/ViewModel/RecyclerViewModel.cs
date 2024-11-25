using PiringPeduliClass.Model;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PiringPeduliWPF.ViewModel
{
    public class RecyclerViewModel: ViewModelBase
    {
        private ObservableCollection<WasteInStorage> _allSortedWaste;
        private ObservableCollection<WasteInStorage> _filteredSortedWaste;
        private ObservableCollection<string> _wasteType;
        private string _selectedWaste;

        public ICommand OrderCommand { get; }

        public ObservableCollection<WasteInStorage> AllSortedWaste
        {
            get => _allSortedWaste;
            set
            {
                _allSortedWaste = value;
                OnPropertyChanged(nameof(AllSortedWaste));
            }
        }

        public ObservableCollection<WasteInStorage> FilteredSortedWaste
        {
            get => _filteredSortedWaste;
            set
            {
                _filteredSortedWaste = value;
                OnPropertyChanged(nameof(FilteredSortedWaste));
            }
        }

        public ObservableCollection<string> WasteType
        {
            get => _wasteType;
            set
            {
                _wasteType = value;
                OnPropertyChanged(nameof(WasteType));
            }
        }

        public string SelectedWaste
        {
            get => _selectedWaste;
            set
            {
                _selectedWaste = value;
                OnPropertyChanged(nameof(SelectedWaste));
                FilterWasteBySelectedWaste(); // Apply filtering
            }
        }

        public RecyclerViewModel()
       {
            AllSortedWaste = new ObservableCollection<WasteInStorage>();
            WasteType = new ObservableCollection<string>();
            FilteredSortedWaste = new ObservableCollection<WasteInStorage>();
            OrderCommand = new ViewModeCommand(Order);
            LoadOrder();
            LoadWasteType();
            FilterWasteBySelectedWaste();

        }

        private void LoadOrder()
        {
            AllSortedWaste.Clear();
            var wastes = DatabaseService.wasteService.GetAllWaste();
            foreach (var waste in wastes)
            {
                AllSortedWaste.Add(waste);
            }
        }

        private void LoadWasteType()
        {
            WasteType.Clear();
            var results = DatabaseService.wasteService.GetAllWasteType();
            foreach (var result in results)
            {
                WasteType.Add(result);
            }
        }

        private void Order(object obj)
        {
            try
            {
                if (obj is WasteInStorage waste)
                {
                    if(waste.SelectQuantity <= 0)
                    {
                        throw new Exception("Please enter a valid quantity");
                    }

                    if(waste.SelectQuantity > waste.Quantity)
                    {
                        throw new Exception("Please enter quantity within stock");
                    }
                    Debug.WriteLine(waste.SelectQuantity);
                    DatabaseService.orderService.CreateOrderRecylcer(StatusType.Processing, waste.Storageid, UserSessionService.Account.AccountId, waste.SelectQuantity);

                    waste.Quantity -= waste.SelectQuantity;
                    DatabaseService.wasteService.UpdateWaste(waste.Storageid, waste.WasteType, waste.Quantity);

                    MessageBox.Show($"Order Success", "Order Succeed", MessageBoxButton.OK, MessageBoxImage.Information);


                    LoadWasteType();
                    FilterWasteBySelectedWaste();
                }

            }catch(Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Order Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void FilterWasteBySelectedWaste()
        {
            // Clear the filtered collection
            FilteredSortedWaste.Clear();
            if (!string.IsNullOrWhiteSpace(SelectedWaste))
            {
                // Filter based on the selected waste type
                var filtered = AllSortedWaste.Where(waste => waste.WasteType == SelectedWaste && waste.Quantity > 0);

                foreach (var waste in filtered)
                {
                    FilteredSortedWaste.Add(waste);
                }
            }
            else
            {
                var filtered = AllSortedWaste.Where(waste => waste.Quantity > 0);

                // If no waste type is selected, display all
                foreach (var waste in filtered)
                {
                    FilteredSortedWaste.Add(waste);
                }
            }
        }
    }
}
