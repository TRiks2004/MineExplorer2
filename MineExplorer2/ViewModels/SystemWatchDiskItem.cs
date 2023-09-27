using System.Management;
using System.Threading.Tasks;

namespace MineExplorer2.ViewModels;

class SystemWatchDiskItem
{
    WqlEventQuery insertQuery = new (
        "SELECT * " +
        "FROM __InstanceCreationEvent WITHIN 2 " +
        "WHERE TargetInstance ISA 'Win32_USBHub'"
    );
    
    WqlEventQuery removeQuery = new (
        "SELECT * " +
        "FROM __InstanceDeletionEvent WITHIN 2 " +
        "WHERE TargetInstance ISA 'Win32_USBHub'"
    );
    
    public delegate void Event(object sender, EventArrivedEventArgs e);
    private Event? _onInsertHandler;
    private Event? _onRemovedHandler;
    
    public event Event? OnInsert 
    {
        add => _onInsertHandler += value;
        remove => _onInsertHandler -= value;
    }

    public event Event? OnRemoved 
    {
        add => _onRemovedHandler += value;
        remove => _onRemovedHandler -= value;
    }
    
    public SystemWatchDiskItem()
    {
        WatchFolder();
    }
    
    public Task WatchFolder()
    {
        var tcs = new TaskCompletionSource<bool>();
        
        
        ManagementEventWatcher removeWatcher = new (removeQuery);
        removeWatcher.EventArrived += _remove;
        removeWatcher.Start();
        
        ManagementEventWatcher insertWatcher = new (insertQuery);
        insertWatcher.EventArrived += _insert;
        insertWatcher.Start();
        
        return tcs.Task;
        
    }
    
    private void _insert(object source, EventArrivedEventArgs e)
    {
        System.Threading.Thread.Sleep(15000);
        _onInsertHandler?.Invoke(source, e);
    }
    
    private void _remove(object source, EventArrivedEventArgs e)
    {
        _onRemovedHandler?.Invoke(source, e);
    }
}