
using System.Windows;
using System.Windows.Input;


namespace TopTalk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState.Minimized;
    }
}