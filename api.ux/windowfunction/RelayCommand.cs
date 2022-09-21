using System;
using System.Windows.Input;

namespace application.api
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool>? canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter != null)
            {
                return this.canExecute == null || this.canExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object? parameter)
        {
            if (parameter != null)
                this.execute(parameter);
        }
    }
}
