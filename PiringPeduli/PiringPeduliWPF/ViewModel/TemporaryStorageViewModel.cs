using PiringPeduliClass.Model;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PiringPeduliWPF.ViewModel
{
    public class TemporaryStorageViewModel:ViewModelBase
    {
        private string _type = "Type";
        private string _weight = "Weight";

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

        public TemporaryStorageViewModel()
        {
            AddCommand = new ViewModeCommand(Add);
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

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Add Waste Failed", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
