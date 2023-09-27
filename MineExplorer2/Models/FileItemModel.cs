using System;

namespace MineExplorer2.Models;

public abstract class FileItemModel
{
    public string NameFile { get; set; }
    public string PathFile { get; set; }
    
    protected FileItemModel(
        string name, 
        string path)
    {
        NameFile = name;
        PathFile = path;
    }
        
}

public sealed class RootDirectoryModel: FileItemModel {
    
    public double Size { get; set; }
    
    public string NameSize { get; set; }
    
    public double FreeSize { get; set; }
    
    public string FreeSizeName { get; set; }
    
    public RootDirectoryModel(
        string name, string path, 
        UnitsConverter size, UnitsConverter freeSize) : base(name, path)
    {
        var sizeAuto = size.ToAuto();
        var freeSizeAuto = freeSize.ToAuto();
        
        Size = Math.Round(sizeAuto.Value, 2);
        NameSize = sizeAuto.Name;
        FreeSize = Math.Round(freeSizeAuto.Value, 2);
        FreeSizeName = freeSizeAuto.Name;
    }
}

public sealed class DirectoryModel: FileItemModel {
    public string RootDirectory { get; set; }
    public DateTime TimeCreated { get; set; }
    public DateTime TimeChanged { get; set; }
    
    public DirectoryModel(
        string name, string path, DateTime timeCreated, DateTime timeChanged, string rootDirectory
        ) : base(name, path)
    {
        RootDirectory = rootDirectory;
        TimeCreated = timeCreated;
        TimeChanged = timeChanged;
    }
}
    
public sealed class FileModel: FileItemModel {
    public FileModel(
        string name, string path) 
        : base(name, path)
    {
    }
}