using System;
using System.Windows.Input;

namespace MarchzinsBonusTool.Commands
{
    /// <summary>
    /// A basic command implementation that relays its functionality to delegates.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Predicate<object?>? canExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
        }

        public bool CanExecute(object? parameter)
        {
        }

        public void Execute(object? parameter)
        {
        }

        public void RaiseCanExecuteChanged()
        {
        }
    }
}