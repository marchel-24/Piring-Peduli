using PiringPeduliWPF.View.UserControls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

public class TemporaryContainerViewModel : INotifyPropertyChanged
{
    private string _jenis;
    private string _berat;

    public string Jenis
    {
        get => _jenis;
        set
        {
            if (_jenis != value)
            {
                _jenis = value;
                OnPropertyChanged(nameof(Jenis));
            }
        }
    }

    public string Berat
    {
        get => _berat;
        set
        {
            if (_berat != value)
            {
                _berat = value;
                OnPropertyChanged(nameof(Berat));
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
        }
    }

    public Visibility TextBoxVisibility => IsEditing ? Visibility.Visible : Visibility.Collapsed;
    public Visibility TextBlockVisibility => IsEditing ? Visibility.Collapsed : Visibility.Visible;

    public ICommand ToggleEditCommand { get; }

    public TemporaryContainerViewModel()
    {
        ToggleEditCommand = new RelayCommand(ToggleEdit);
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
}
