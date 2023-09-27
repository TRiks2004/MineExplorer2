using System;
using System.Collections.ObjectModel;
using System.IO;
using MineExplorer.ViewModels;
using MineExplorer2.Models;

namespace MineExplorer2.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ObservableCollection<RootDirectoryModel> Disk { get; set; } = new ();
    public ObservableCollection<DirectoryModel> Folder { get; set; } = new ();
    public ObservableCollection<FileModel> File { get; set; } = new ();

    public DirectoryModel? SelectDirectory { get; set; }
    
    private string _selectDirectoryPath = "";
    
    public string SelectDirectoryPath
    {
        get => _selectDirectoryPath;
        set
        {
            _selectDirectoryPath = value;
            OnPropertyChanged(); 
        }
    }
    
    
    private string _selectDirectoryCreateTime = "";
    public string SelectDirectoryCreateTime
    {
        get => _selectDirectoryCreateTime;
        set
        {
            _selectDirectoryCreateTime = value;
            OnPropertyChanged(); 
        }
    }
    
    private string _selectDirectoryRoot = "";
    public string SelectDirectoryRoot
    {
        get => _selectDirectoryRoot;
        set
        {
            _selectDirectoryRoot = value;
            OnPropertyChanged(); 
        }
    }
    
    private string directoryPath = @"C:\";

    private readonly SystemWatchFileItem _systemWatchDisk;
    private readonly SystemWatchFileItem _systemWatchFile = new ();
    
    public MainViewModel()
    {
        // _systemWatchDisk = new SystemWatchFileItem(directoryPath);
        // _systemWatchDisk.OnChanged += OnChanged;

        var _systemWatchDiskItem = new SystemWatchDiskItem();
        
        _systemWatchDiskItem.OnInsert += (sender, args) =>
        {
            FullDisk();
        };
        
        _systemWatchDiskItem.OnRemoved += (sender, args) =>
        {
            FullDisk();
        };
        
        
        
        FullDisk();
        // FullMainDisk();
    }

    public void FullDisk()
    {
        Disk.Clear();
        
        var disks = DriveInfo.GetDrives();

        foreach (var driveInfo in disks)
        {
            Disk.Add(
                new RootDirectoryModel(
                    driveInfo.Name,
                    driveInfo.RootDirectory.Name,
                    new UnitsConverter (driveInfo.TotalSize),
                    new UnitsConverter(driveInfo.AvailableFreeSpace)
                )
            );
        }
    }


    public void FullMainDisk()
    {
        Folder.Clear();
        
        var folders = Directory.GetDirectories(directoryPath);

        foreach (var folder in folders)
        {
            var directoryInfo = new DirectoryInfo(folder);
            Folder.Add(new DirectoryModel(
                    directoryInfo.Name,
                    directoryInfo.FullName,
                    directoryInfo.CreationTime,
                    directoryInfo.LastWriteTime,
                    Directory.GetDirectoryRoot(folder)
                )
            );
        }
    }
    
    private void OnChanged(object source, FileSystemEventArgs e)
    {
        FullMainDisk();
    }
    
    public void OneClickDirectory(DirectoryModel? selectDirectory)
    {
        
        SelectDirectory = selectDirectory;
        File.Clear();
        
        if (SelectDirectory is not null)
        {
            try
            {
                _systemWatchFile.Path = SelectDirectory.PathFile;
            
                _systemWatchFile.OnChanged += OnChangedFile;
            
                SelectDirectoryPath = SelectDirectory.PathFile;
                SelectDirectoryCreateTime = SelectDirectory.TimeCreated.ToString("MM/dd/yy H:mm:ss zzz");
                SelectDirectoryRoot = SelectDirectory.RootDirectory;
            
                var files = new DirectoryInfo(SelectDirectory.PathFile).GetFiles();
            
                foreach (var fileInfo in files)
                {
                    File.Add(
                        new FileModel(
                            fileInfo.Name, 
                            fileInfo.FullName
                        )
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }

    private void OnChangedFile(object sender, FileSystemEventArgs e)
    {
        OneClickDirectory(SelectDirectory);
    }
}