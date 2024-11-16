using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.ViewModel;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using PiringPeduliClass.Model;

namespace PiringPeduliWPF.View.Windows
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        public SignUpView()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDB"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var userRepository = new AccountRepository(connectionString);
            var userService = new AccountService(userRepository);
            var courierRepository = new CourierRepository(connectionString);
            var courierService = new CourierService(courierRepository);
            DataContext = new SignUpViewModel(userService, courierService);
        }

        private void BtnMinimized_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClosed_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is SignUpViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password; // Update the ViewModel
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var confirmPasswordBox = sender as PasswordBox;
            if (DataContext is SignUpViewModel viewModel)
            {
                viewModel.ConfirmPassword = confirmPasswordBox.Password; // Update the ViewModel
            }
        }

        private void ArrowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            QuestionComboBox.IsDropDownOpen = !QuestionComboBox.IsDropDownOpen;
        }


        private void QuestionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ambil pilihan yang dipilih dari ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)QuestionComboBox.SelectedItem;
            string selectedOption = selectedItem.Content.ToString();

            // Sembunyikan semua elemen terlebih dahulu
            VehicleTypeComboBox.Visibility = Visibility.Collapsed;
            CourierNameTextBox.Visibility = Visibility.Collapsed;
            VehicleType.Visibility = Visibility.Collapsed;
            CourierName.Visibility = Visibility.Collapsed;

            CustomerNameTextBox.Visibility = Visibility.Collapsed;
            CustomerInstanceTextBox.Visibility = Visibility.Collapsed;
            CustomerAddressTextBox.Visibility = Visibility.Collapsed;
            CustomerName.Visibility = Visibility.Collapsed;
            CustomerInstance.Visibility = Visibility.Collapsed;
            CustomerAddress.Visibility = Visibility.Collapsed;
          

            RecyclerNameTextBox.Visibility = Visibility.Collapsed;
            RecyclerAddressTextBox.Visibility = Visibility.Collapsed;
            RecyclerName.Visibility = Visibility.Collapsed;
            RecyclerAddress.Visibility = Visibility.Collapsed;

            TempStorageNameTextBox.Visibility = Visibility.Collapsed;
            TempStorageAddressTextBox.Visibility = Visibility.Collapsed;
            TemporaryStorage.Visibility = Visibility.Collapsed;
            TemporaryStorageAddress.Visibility = Visibility.Collapsed;

            // Tampilkan elemen yang sesuai dengan pilihan
            switch (selectedOption)
            {
                case "Courier":
                    VehicleTypeComboBox.Visibility = Visibility.Visible;
                    CourierNameTextBox.Visibility = Visibility.Visible;
                    VehicleType.Visibility= Visibility.Visible;
                    CourierName.Visibility= Visibility.Visible;
                    CourierName.Text = "Courier Name";
                    VehicleType.Text = "Vehicle Type";
                    
                    VehicleTypeComboBox.SelectionChanged += (s, args) =>
                    {
                        if (DataContext is SignUpViewModel vm)
                        {
                            vm.AccountTypeStr = selectedOption;
                            ComboBoxItem selectedVehicle = (ComboBoxItem)VehicleTypeComboBox.SelectedItem;
                            vm.VehicleTypestr = selectedVehicle?.Content.ToString();
                        }
                    };

                    CourierNameTextBox.TextChanged += (s, args) =>
                    {
                        if (DataContext is SignUpViewModel vm)
                        {
                            vm.CourierName = CourierNameTextBox.Text;
                        }
                    };
                    break;

                case "Customer":
                    CustomerNameTextBox.Visibility = Visibility.Visible;
                    CustomerInstanceTextBox.Visibility = Visibility.Visible;
                    CustomerAddressTextBox.Visibility = Visibility.Visible;
                    CustomerName.Visibility= Visibility.Visible;
                    CustomerInstance.Visibility = Visibility.Visible;
                    CustomerAddress.Visibility= Visibility.Visible;
                    CustomerName.Text = "Customer Name";
                    CustomerInstance.Text = "Instance";
                    CustomerAddress.Text = "Customer Address";
                    break;

                case "Recycler":
                    RecyclerNameTextBox.Visibility = Visibility.Visible;
                    RecyclerAddressTextBox.Visibility = Visibility.Visible;
                    RecyclerName.Visibility= Visibility.Visible;
                    RecyclerAddress.Visibility= Visibility.Visible;
                    RecyclerName.Text = "Recycler Name";
                    RecyclerAddress.Text = "Recycler Address";
                    break;

                case "Temporary Storage":
                    TempStorageNameTextBox.Visibility = Visibility.Visible;
                    TempStorageAddressTextBox.Visibility = Visibility.Visible;
                    TemporaryStorage.Visibility= Visibility.Visible;
                    TemporaryStorageAddress.Visibility= Visibility.Visible;
                    TemporaryStorage.Text = " Name Temporary Storage";
                    TemporaryStorageAddress.Text = "Temporary Storage Address";
                    break;
            }
        }



        private void VehicleType_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            VehicleTypeComboBox.IsDropDownOpen = !VehicleTypeComboBox.IsDropDownOpen;
        }

        private void VehicleTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ambil pilihan yang dipilih dari ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)VehicleTypeComboBox.SelectedItem;
            string selectedOption = selectedItem.Content.ToString();

            // Update pertanyaan kedua berdasarkan pilihan
            switch (selectedOption)
            {
                case "Truck":
                    break;
                case "Car":
                    break;
                case "Motorcycle": 
                    break;
                
            }
        }

    }
}
