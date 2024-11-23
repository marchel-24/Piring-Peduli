using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using PiringPeduliClass.Model;
using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;

namespace PiringPeduliWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new DatabaseService();

        }
    }

}
