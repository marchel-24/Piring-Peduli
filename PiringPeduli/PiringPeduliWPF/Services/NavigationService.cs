using PiringPeduliWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PiringPeduliWPF.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Window _currentWindow;

        public NavigationService(Window currentWindow)
        {
            _currentWindow = currentWindow;
        }

        public void NavigateTo(string viewName)
        {
            // Example: Depending on the view name, create the corresponding view and show it
            Window newView = viewName switch
            {
                "LoginView" => new LoginView(),  // Assuming you have a LoginView.xaml
                "SignUpView" => new SignUpView(),
                "HomeScreenView" => new HomeScreen(),
                _ => throw new ArgumentException("Unknown view", nameof(viewName))
            };

            _currentWindow.Close(); // Close the current window
            newView.Show();         // Open the new window
        }
    }
}
