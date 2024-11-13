using PiringPeduliWPF.Services;
using PiringPeduliClass.Model;
using PiringPeduliWPF.ViewModel;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = SettingViewModel.Instance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var parentFrame = FindParent<SettingsFrame>(this);
            if (parentFrame != null)
            {
                if (UserSessionService.Account.Type == AccountType.Customer)
                {
                    parentFrame.ContentControl.Content = new UpdateProfileCustomer();
                }
                else if (UserSessionService.Account.Type == AccountType.Recycler)
                {
                    parentFrame.ContentControl.Content = new UpdateProfileRecycler();
                }
                else if (UserSessionService.Account.Type == AccountType.TemporaryStorage)
                {
                    parentFrame.ContentControl.Content = new UpdateProfileTemporaryStorage();
                }
                else if (UserSessionService.Account.Type == AccountType.Courier)
                {
                    parentFrame.ContentControl.Content = new UpdateProfileCourier();
                }
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
    }
}
