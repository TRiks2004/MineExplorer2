using System;
using System.Windows.Input;

namespace MineExplorer2.ViewModels;

public class OneClickDirectoryCommand : ICommand
{
    private readonly Action<object> _oneClickDirectory;

    public OneClickDirectoryCommand(Action<object> oneClickDirectory)
    {
        _oneClickDirectory = oneClickDirectory;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _oneClickDirectory.Invoke(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}