using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliWPF.Services
{
    public class UserSessionService
    {
        private static Account _account;

        // Private constructor to prevent instantiation from other classes
        private UserSessionService() { }

        // Public static property to access the instance
        public static Account Account
        {
            get => _account;
            set => _account = value;
        }

        public static void LogOut()
        {
            if (_account != null)
            {
                _account = null;
            }
            NavigationService.NavigateTo("LoginView");
        }
    }
}
