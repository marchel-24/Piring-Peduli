using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;


namespace PiringPeduliWPF.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        private static SettingViewModel _instance;
        public static SettingViewModel Instance => _instance ??= new SettingViewModel();

        private readonly Notifier _notifier;


        public SettingViewModel()
        {
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 40);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
        private bool _isNotificationEnabled;
        public bool IsNotificationEnabled
        {
            get => _isNotificationEnabled;
            set
            {
                if (_isNotificationEnabled != value)
                {
                    _isNotificationEnabled = value;
                    OnPropertyChanged(nameof(IsNotificationEnabled));

                    // Tampilkan notifikasi ketika diaktifkan
                    if (_isNotificationEnabled)
                    {
                        ShowNotification("Notifikasi Nyala");
                    }
                }
            }
        }

        private void ShowNotification(string message)
        {
            _notifier.ShowInformation(message);
        }
    }
}
