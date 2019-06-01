using System.Windows;

namespace SingleInstance.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;

            App.SingleInstance.OnNewInstance += SingleInstance_OnNewInstance;
        }

        private void SingleInstance_OnNewInstance()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                BringToFront();
                MessageBox.Show("Can not open another app instance!");
            });
        }

        private void BringToFront()
        {
            if (!IsVisible)
            {
                Show();
            }

            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            Activate();
            Topmost = true;
            Topmost = false;
            Focus();
        }
    }
}