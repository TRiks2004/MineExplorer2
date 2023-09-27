using Avalonia.Controls;
using MineExplorer2.Models;
using MineExplorer2.ViewModels;

namespace MineExplorer2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void SelectingItemsControl_OnSelectionChanged(object? o, SelectionChangedEventArgs e)
    {
        var selectedItem = o as ListBox;
        var selectedItems = selectedItem?.SelectedItem;
        
        if (selectedItems is null) return;
        
        var selectDirectory = selectedItems as DirectoryModel;
        var mainViewModel = DataContext as MainViewModel;
        mainViewModel?.OneClickDirectory(selectDirectory);
    }

    private void Combobox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selectedItem = sender as ComboBox;   
        var selectedItems = selectedItem?.SelectedItem;

        if (selectedItems is null) return;
       
        
        var selectDirectory = selectedItems as RootDirectoryModel;
    }
}