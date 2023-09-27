using System.IO;
using System.Threading.Tasks;

namespace MineExplorer2.ViewModels;

public class SystemWatchFileItem
{
    private FileSystemWatcher _watcher = new();

    private string? _path;
    
    public string? Path 
    {
        get => _path;
        set
        {
            if (value == null) return;
           
            if (_path is not null)
            {
                _watcher.Path = _path =  value;
            }
            else
            {
                _watcher.Path = _path = value;
                WatchFolder();
            }
        }
    }

    public delegate void Changed(object sender, FileSystemEventArgs e);
    private Changed? _onChangedHandler;

    public event Changed? OnChanged 
    {
        add => _onChangedHandler += value;
        remove => _onChangedHandler -= value;
    }

    public SystemWatchFileItem() { }

    public SystemWatchFileItem(string folderPath)
    {
        Path = folderPath;
    }
    
    public Task WatchFolder()
    {
       
        var tcs = new TaskCompletionSource<bool>();
        // watcher.Filter = "*.*";
        _watcher.Changed += _onChanged;
        _watcher.Created += _onChanged;
        _watcher.Deleted += _onChanged;
        _watcher.Renamed += _onChanged;
        _watcher.EnableRaisingEvents = true;
        
        return tcs.Task;
    }
    
    private void _onChanged(object source, FileSystemEventArgs e)
    {
        _onChangedHandler?.Invoke(source, e);
    }
}