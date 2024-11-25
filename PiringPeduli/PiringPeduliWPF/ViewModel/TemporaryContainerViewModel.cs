using PiringPeduliClass.Model;
using PiringPeduliWPF.Services;
using PiringPeduliWPF.View.UserControls;
using PiringPeduliWPF.ViewModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

public class TemporaryContainerViewModel : INotifyPropertyChanged
{
    private string _type;
    private string _weight;
    private TemporaryStorageViewModel _viewModel;

    public string Type
    {
        get => _type;
        set
        {
            if (_type != value)
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
    }

    public string Weight
    {
        get => _weight;
        set
        {
            if (_weight != value)
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }
    }

    private bool _isEditing;
    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            _isEditing = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(TextBoxVisibility));
            OnPropertyChanged(nameof(TextBlockVisibility));

            // Debugging the state change
            DebugStateChange();
        }
    }

    public Visibility TextBoxVisibility => IsEditing ? Visibility.Visible : Visibility.Collapsed;
    public Visibility TextBlockVisibility => IsEditing ? Visibility.Collapsed : Visibility.Visible;

    public ICommand ToggleEditCommand { get; }
    public ICommand ToggleDeleteCommand { get; }

    public TemporaryContainerViewModel(WasteInStorage waste, TemporaryStorageViewModel viewModel)
    {
        // Initialize the properties with the values from WasteInStorage
        Type = waste.WasteType;
        Weight = waste.Quantity.ToString();
        _viewModel = viewModel;

        ToggleEditCommand = new RelayCommand(ToggleEdit);
        ToggleDeleteCommand = new ViewModeCommand(Delete);
    }

    private void ToggleEdit()
    {
        IsEditing = !IsEditing;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void DebugStateChange()
    {
        // When toggling from editing to non-editing, print the values of Type and Weight
        if (!IsEditing)
        {
            DatabaseService.wasteService.UpdateWaste(UserSessionService.Account.AccountId, Type, Convert.ToDouble(Weight));
            MessageBox.Show($"Edit Waste Success", "Edit Waste Succeed", MessageBoxButton.OK, MessageBoxImage.Information);

        }
        else
        {
        }
    }

    private void Delete(object obj)
    {
        DatabaseService.wasteService.DeleteWaste(UserSessionService.Account.AccountId, Type);
        MessageBox.Show($"Delete Waste Success", "Delete Waste Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
        _viewModel.LoadWaste();
    }
}
