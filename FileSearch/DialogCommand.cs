#region License

// Copyright (c) 2016, Vira
// All rights reserved.
// Solution: FileSearch
// Project: FileSearch
// Filename: DialogCommand.cs
// Date - created:2016.07.16 - 09:23
// Date - current: 2016.08.17 - 16:28

#endregion

#region Usings

using System;
using System.Windows.Input;

#endregion

namespace FileSearch
{
    internal class DialogCommand : ICommand
    {
        private readonly Action<object> _myAction;

        public DialogCommand(Action<object> action)
        {
            _myAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _myAction(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}