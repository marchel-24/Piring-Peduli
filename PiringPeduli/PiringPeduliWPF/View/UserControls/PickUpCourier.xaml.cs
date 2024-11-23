using Microsoft.Maps.MapControl.WPF;
using PiringPeduliWPF.View.Component;
using System;
using System.Collections;
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
            var apikey = ConfigurationManager.AppSettings["BingMapsApiKey"];
            BingMap.CredentialsProvider = new ApplicationIdCredentialsProvider(apikey);
            LoadOrder();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadOrder()
        {
            var Data = new[]
            {
                new{Asal = "Jauh", Type= "Small", Deskripsi = "Lumayan Banyak ni", Tujuan = "Dekat"},
                new{Asal = "Mana", Type = "Bagus", Deskripsi = "Ini adalah salah satu contoh deskripsi yang lumayan panjangppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppppp", Tujuan = "Sini"}
            };

            foreach (var item in Data)
            {
                var container = new CourierContainer
                {
                    DataContext = item
                };
                OrderList.Children.Add(container);
            }
        }
    }
}
