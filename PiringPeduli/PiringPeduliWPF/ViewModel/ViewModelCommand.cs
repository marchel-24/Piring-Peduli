using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PiringPeduliWPF.ViewModel
{
    public class ViewModeCommand : ICommand
    {
        //Fields
        private readonly Action<object> _executeAction;
        private readonly Func<object, Task> _executeAsync;
        private readonly Predicate<object> _canExecuteAction;

        public ViewModeCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
            _executeAsync = null;
            _canExecuteAction = null;
        }

        public ViewModeCommand(Func<object, Task> executeAsync)
        {
            _executeAsync = executeAsync;
            _executeAction = null;
            _canExecuteAction = null;
        }


        public ViewModeCommand(Action<object> executeAction, Predicate<object> canExecuteAction) : this(executeAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
            if (_executeAsync != null)
                await _executeAsync(parameter);
        }
    }
}