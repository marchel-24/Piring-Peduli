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
    public class TemporaryStorageViewModel:ViewModelBase
    {
        private string _type = "Type";
        private string _weight = "Weight";
        private ObservableCollection<TemporaryContainerViewModel> _waste;


        public ICommand AddCommand { get; }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(Type);
            }
        }

        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                OnPropertyChanged(Weight);
            }
        }

        public ObservableCollection<TemporaryContainerViewModel> Waste
        {
            get => _waste;
            set
            {
                _waste = value;
                OnPropertyChanged(nameof(Waste));
            }
        }

        public TemporaryStorageViewModel()
        {
            AddCommand = new ViewModeCommand(Add);
            Waste = new ObservableCollection<TemporaryContainerViewModel>();
            LoadWaste();
        }

        private void Add(object obj)
        {
            try
            {
                // Validate if the username is provided
                if (string.IsNullOrWhiteSpace(Type) || Type == "Type")
                {
                    throw new Exception("Type is required.");
                }

                // Validate if the password is provided and meets the length requirement
                if (string.IsNullOrWhiteSpace(Weight) || Weight == "Weight")
                {
                    throw new Exception("Weight is required.");
                }
                double weightDbl = Convert.ToDouble(Weight);

                if(weightDbl < 0)
                {
                    throw new Exception("Please input a valid weight");
                }
                DatabaseService.wasteService.AddWaste(UserSessionService.Account.AccountId, Type, weightDbl);
                MessageBox.Show($"Add Waste Success", "Add Wste Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadWaste();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Add Waste Failed", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void LoadWaste()
        {
            Waste.Clear();
            var wastes = DatabaseService.wasteService.GetWaste(UserSessionService.Account.AccountId);
            foreach (var waste in wastes)
            {
                Waste.Add(new TemporaryContainerViewModel(waste, this));
            }
        }
    }
}
