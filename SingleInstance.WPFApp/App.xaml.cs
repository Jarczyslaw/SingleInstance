using System;
using System.Windows;

namespace SingleInstance.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SingleInstanceManager SingleInstance { get; } = new SingleInstanceManager("a78dd11e-aec3-4c68-aa5a-4410c51d45fd", TimeSpan.Zero);

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!SingleInstance.FirstInstance)
            {
                SingleInstance.SendNofitication();
                Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SingleInstance.Dispose();
        }
    }
}