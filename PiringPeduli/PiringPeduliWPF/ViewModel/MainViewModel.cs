using PiringPeduliWPF.Services;
using PiringPeduliClass.Service;
using PiringPeduliClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Diagnostics;

namespace PiringPeduliWPF.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private Account _account;

        public Account Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged(nameof(Account));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get 
            { 
                return _currentChildView; 
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public ICommand ShowMainScreenViewCommand { get; }
        public ICommand ShowAccountViewCommand { get; }
        public ICommand ShowPickUpViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public MainViewModel()
        {
            Account = UserSessionService.Account;
            ShowMainScreenViewCommand = new ViewModeCommand(ExecuteMainScreenView);
            ShowPickUpViewCommand = new ViewModeCommand(ExecutePickUpView);
            ShowAccountViewCommand = new ViewModeCommand(ExecuteAccountView);
            ShowSettingsViewCommand = new ViewModeCommand(ExecuteSettingsView);


            ExecuteMainScreenView(null);
        }

        private void ExecuteMainScreenView(object obj)
        {
            CurrentChildView = new MainScreenViewModel();
        }

        private void ExecutePickUpView(object obj)
        {
            if (UserSessionService.Account.Type == AccountType.Customer)
            {
                CurrentChildView = new PickUpViewModel();
            }
            else if (UserSessionService.Account.Type == AccountType.Courier)
            {
                CurrentChildView = new PickUpCourierViewModel();
            }
            else if (UserSessionService.Account.Type == AccountType.TemporaryStorage)
            {
                CurrentChildView = new TemporaryStorageViewModel();
            }
            else if (UserSessionService.Account.Type == AccountType.Recycler)
            {
                CurrentChildView = new RecyclerViewViewModel();
            }
        }

        private void ExecuteAccountView(object obj)
        {
            CurrentChildView = new AccountViewModel();
        }

        private void ExecuteSettingsView(object obj)
        {
            CurrentChildView = new SettingViewModel();
        }
    }
}
