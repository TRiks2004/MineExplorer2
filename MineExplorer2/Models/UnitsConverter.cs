using System;

namespace MineExplorer2.Models;

public enum UnitsType {
    Byte = 0, 
    Kilobyte = 1, 
    Megabyte = 2, 
    Gigabyte = 3, 
    Terabyte = 4, 
    Petabyte = 5
}

public struct UnitsInfo {
    public double DValue;
    public double Value;
    
    public UnitsType Type;
    
    public string Name;
    public string ShortName;

    public UnitsInfo(double value, UnitsType type, double dValue)
    {
        Type = type;
        Value = value;

        DValue = dValue;
        
        if (Type == UnitsType.Byte)
        {
            Name = "byte";
            ShortName = "b";
        }

        if (Type == UnitsType.Kilobyte)
        {
            Name = "kilobyte";
            ShortName = "kb";
            
        }

        if (Type == UnitsType.Megabyte)
        {
            Name = "megabyte";
            ShortName = "mb";
        }

        if (Type == UnitsType.Gigabyte)
        {
            Name = "gigabyte";
            ShortName = "gb";
        }

        if (Type == UnitsType.Terabyte)
        {
            Name = "terabyte";
            ShortName = "tb";
        }

        if (Type == UnitsType.Petabyte)
        {
            Name = "petabyte";
            ShortName = "pb";
        }
    }
}

public class UnitsConverter {
    private readonly long _byte;

    public UnitsConverter(long byt)
    {
        _byte = byt;
    }

    private UnitsInfo ToType(UnitsType type)
    {
        return new UnitsInfo(_byte / Math.Pow(1024, (int)type), type, _byte);
    }
        
    public UnitsInfo ToKilobyte() => ToType(UnitsType.Kilobyte);
    public UnitsInfo ToMegabyte() => ToType(UnitsType.Megabyte);
    public UnitsInfo ToGigabyte() => ToType(UnitsType.Gigabyte);
    public UnitsInfo ToTerabyte() => ToType(UnitsType.Terabyte);
    public UnitsInfo ToPetabyte() => ToType(UnitsType.Petabyte);

    public UnitsInfo ToAuto()
    {
        var suffixes = Enum.GetValues(typeof(UnitsType));
        var index = 0;
        double bytes = _byte;
        while (bytes >= 1024 && index < suffixes.Length - 1)
        {
            bytes /= 1024;
            index++;
        }
        
        return new UnitsInfo(bytes, (UnitsType)(suffixes.GetValue(index) ?? UnitsType.Byte), _byte);
    }
        
}