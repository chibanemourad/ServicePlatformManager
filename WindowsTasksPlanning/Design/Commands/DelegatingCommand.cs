using System;
using System.Windows.Input;
using System.Windows.Threading;
using WindowsTasksPlanning.Infrastructure.Impl;

namespace WindowsTasksPlanning.Design.Commands
{
    public class DelegatingCommand : ICommand 
    {
        private readonly Action _action;
        private readonly Fact _canExecute;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute.Value;
        }

        public DelegatingCommand(Action action, Fact canExecute)
        {
            _action = action;
            _canExecute = canExecute;
            var dispatcher = Dispatcher.CurrentDispatcher;
            if (canExecute != null)
            {
                _canExecute.PropertyChanged +=
                    (sender, args) =>
                        dispatcher.Invoke(CanExecuteChanged, this, EventArgs.Empty);
            }
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged = delegate { };
    }
}
