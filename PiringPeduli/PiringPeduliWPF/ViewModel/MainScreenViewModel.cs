using PiringPeduliClass.Model;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliWPF.ViewModel
{
    public class MainScreenViewModel:ViewModelBase
    {
        private string _username;
        private string _accounttype;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string accountType
        {
            get => _accounttype;
            set
            {
                _accounttype = value;
                OnPropertyChanged(nameof(AccountType));
            }
        }

        public MainScreenViewModel()
        {
            Username = UserSessionService.Account.Username;
            accountType = UserSessionService.Account.Type.ToString();
        }


    }
}
