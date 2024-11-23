using Microsoft.Maps.MapControl.WPF;
using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PiringPeduliWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for PickUp.xaml
    /// </summary>
    public partial class PickUp : UserControl
    {
        public PickUp()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDb"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var orderRepository = new OrderRepository(connectionString);
            var accountRepository = new AccountRepository(connectionString);
            var temporaryStorageRepository = new TemporaryStorageRepository(connectionString);
            var orderService = new OrderService(orderRepository, accountRepository);
            var temporaryServiceStorage = new TemporaryStorageService(temporaryStorageRepository);
            DataContext = new PickUpViewModel(orderService, temporaryServiceStorage);

            var apiKey = ConfigurationManager.AppSettings["BingMapsApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("API Key Bing Maps tidak ditemukan. Tambahkan ke App.config.");
                return;
            }

            BingMap.CredentialsProvider = new ApplicationIdCredentialsProvider(apiKey);


            Loaded += async (s, e) => await InitializeMapPinsAsync();

        }

        private void TxtWeight_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddPushpin(double latitude, double longitude)
        {
            // Create a pushpin
            var pushpin = new Pushpin
            {
                Location = new Location(latitude, longitude)
            };

            // Add the pushpin to the PinLayer
            PinLayer.Children.Add(pushpin);
        }

        private async Task InitializeMapPinsAsync()
        {
            try
            {

                if (DataContext is PickUpViewModel viewModel)
                {
                    // Fetch temporary storage data
                    if (viewModel.LoadTemporaryStorageCommand.CanExecute(null))
                    {
                        await ((ViewModeCommand)viewModel.LoadTemporaryStorageCommand).ExecuteAsync(null); // Triggers LoadTemporaryStorageAsync
                    }

                    // Add pins for each temporary storage
                    Debug.WriteLine(viewModel.TemporaryStorages.Count);
                    foreach (var storage in viewModel.TemporaryStorages)
                    {
                        AddPushpin(storage.Lat, storage.Lon);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading map pins: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ArrowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            QuestionComboBox.IsDropDownOpen = !QuestionComboBox.IsDropDownOpen;
        }

        private void DescriptionBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DescriptionBox.Text == "Enter your description...")
            {
                DescriptionBox.Text = string.Empty;
                DescriptionBox.Foreground = new SolidColorBrush(Colors.Black);  // Warna teks ketika mulai mengetik
            }
        }

        private void DescriptionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                DescriptionBox.Text = "Enter your description...";  // Placeholder
                DescriptionBox.Foreground = new SolidColorBrush(Colors.Gray);  // Warna placeholder
            }
        }

        private void QuestionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ambil pilihan yang dipilih dari ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)QuestionComboBox.SelectedItem;
            string selectedOption = selectedItem.Content.ToString();

            Debug.WriteLine(selectedOption);

            if (DataContext is PickUpViewModel viewModel)
            {
                viewModel.SizeStr = selectedOption;
            }
        }
    }
}
