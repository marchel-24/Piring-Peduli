using System;
using System.Collections.Generic;
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
    /// Interaction logic for SettingsFrame.xaml
    /// </summary>
    public partial class SettingsFrame : UserControl
    {
        public ContentControl ContentControl => contentControl;
        public SettingsFrame()
        {
            InitializeComponent();
            switchSettingsMain();
        }

        private void switchSettingsMain()
        {
            contentControl.Content = new Settings();
        }
    }
}
