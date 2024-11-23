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
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfileTemporaryStorage : UserControl
    {
        private Pushpin currentPushpin;

        public UpdateProfileTemporaryStorage()
        {
            InitializeComponent();
            DataContext = new UpdateProfileStorageViewModel();
            //Debug.Print((UpdateProfileViewModel)DataContext.));

            var apiKey = ConfigurationManager.AppSettings["BingMapsApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("API Key Bing Maps tidak ditemukan. Tambahkan ke App.config.");
                return;
            }

            BingMap.CredentialsProvider = new ApplicationIdCredentialsProvider(apiKey);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is UpdateProfileStorageViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password; // Update the ViewModel
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var confirmPasswordBox = sender as PasswordBox;
            if (DataContext is UpdateProfileStorageViewModel viewModel)
            {
                viewModel.ConfirmPassword = confirmPasswordBox.Password; // Update the ViewModel
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var parentFrame = FindParent<SettingsFrame>(this);
            if (parentFrame != null)
            {
                parentFrame.ContentControl.Content = new SettingsFrame();
            }
        }

        private void BingMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Konversi titik piksel ke lokasi geografis (koordinat)
            var mousePosition = e.GetPosition(BingMap);
            Location pinLocation = BingMap.ViewportPointToLocation(mousePosition);

            // Tampilkan koordinat di TextBlock
            Coordinate.Text = $"Latitude: {pinLocation.Latitude}, Longitude: {pinLocation.Longitude}";

            if (DataContext is UpdateProfileStorageViewModel viewModel)
            {
                viewModel.Lat = pinLocation.Latitude;
                viewModel.Lon = pinLocation.Longitude;
            }

            // Tambahkan atau pindahkan pin ke lokasi yang diklik
            UpdatePushpin(pinLocation);
        }

        private void UpdatePushpin(Location location)
        {
            // Jika pin sudah ada, pindahkan ke lokasi baru
            if (currentPushpin != null)
            {
                currentPushpin.Location = location;
            }
            else
            {
                // Jika belum ada pin, buat pin baru dan tambahkan ke peta
                currentPushpin = new Pushpin
                {
                    Location = location
                };
                BingMap.Children.Add(currentPushpin);
            }
        }
    }
}
