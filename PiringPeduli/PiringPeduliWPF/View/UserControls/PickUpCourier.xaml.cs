using Microsoft.Maps.MapControl.WPF;
using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.View.Component;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections;
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
    /// Interaction logic for PickUpCourier.xaml
    /// </summary>
    public partial class PickUpCourier : UserControl
    {
        public PickUpCourier()
        {
            InitializeComponent();
            DataContext = new PickUpCourierViewModel(this);

            var apikey = ConfigurationManager.AppSettings["BingMapsApiKey"];
            BingMap.CredentialsProvider = new ApplicationIdCredentialsProvider(apikey);

            Loaded +=  (s, e) => InitializeMapPinsAsync();
        }

        public void InitializeMapPinsAsync()
        {
            try
            {
                if (DataContext is PickUpCourierViewModel viewModel)
                {
                    PinLayer.Children.Clear();
                    // Fetch temporary storage data
                    if (viewModel.LoadSourceDestinationCommand.CanExecute(null))
                    {
                        ((ViewModeCommand)viewModel.LoadSourceDestinationCommand).Execute(null); // Triggers LoadTemporaryStorageAsync
                    }

                    if(viewModel.SourceAccount != null && viewModel.DestinationAccount != null)
                    {
                        AddPushpin(viewModel.SourceAccount.Lat, viewModel.SourceAccount.Lon, Colors.Blue);
                        AddPushpin(viewModel.DestinationAccount.Lat, viewModel.DestinationAccount.Lon, Colors.Red);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading map pins: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPushpin(double latitude, double longitude, Color color)
        {
            // Create a pushpin
            var pushpin = new Pushpin
            {
                Location = new Location(latitude, longitude),
                Background = new SolidColorBrush(color)
            };

            // Add the pushpin to the PinLayer
            PinLayer.Children.Add(pushpin);
        }
    }
}
