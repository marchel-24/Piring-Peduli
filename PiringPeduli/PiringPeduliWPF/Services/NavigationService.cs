using PiringPeduliWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PiringPeduliWPF.Services
{
    public class NavigationService
    {
        private static Window _currentWindow;

        private NavigationService() { }

        public static Window CurrentWindow
        {
            get => _currentWindow;
            set => _currentWindow = value;
        }

        public static void NavigateTo(string viewName)
        {
            // Example: Depending on the view name, create the corresponding view and show it
            Window newView = viewName switch
            {
                "LoginView" => new LoginView(),  // Assuming you have a LoginView.xaml
                "SignUpView" => new SignUpView(),
                "HomeScreenView" => new HomeScreen(),
                _ => throw new ArgumentException("Unknown view", nameof(viewName))
            };

            if (_currentWindow != null)
            {

                Debug.Print(CurrentWindow.ToString());
                _currentWindow.Close(); // Close the current window
            }
            newView.Show();         // Open the new window
            _currentWindow = newView;
        }
    }
}
