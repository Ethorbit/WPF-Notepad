using System;
using System.Windows.Input;

namespace SimpleViewModel2.Commands
{
    class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Action executeBlank;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public RelayCommand(Action execute)
        {
            this.executeBlank = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
                this.execute(parameter);
            else
                this.executeBlank();
        }
    }
}
