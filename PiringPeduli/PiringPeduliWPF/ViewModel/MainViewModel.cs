﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace PiringPeduliWPF.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private ViewModelBase _currentChildView;

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

        public MainViewModel()
        {
            ShowMainScreenViewCommand = new ViewModeCommand(ExecuteMainScreenView);
            ShowPickUpViewCommand = new ViewModeCommand(ExecutePickUpView);
            ShowAccountViewCommand = new ViewModeCommand(ExecuteAccountView);
        }

        private void ExecuteMainScreenView(object obj)
        {
            CurrentChildView = new MainScreenViewModel();
        }

        private void ExecutePickUpView(object obj)
        {
            CurrentChildView = new PickUpViewModel();
        }

        private void ExecuteAccountView(object obj)
        {
            CurrentChildView = new AccountViewModel();
        }
    }
}
