﻿
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TopTalk.ViewModels;


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
            TopTalkClientVm.Init();
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }
        private void HidePopup(object sender, MouseEventArgs e)
        {
            Popup.Visibility = Visibility.Collapsed;
            Popup.IsOpen = false;
        }
        private void ShowPopup(UIElement element, string text)
        {
            if (Tg_Btn.IsChecked == false)
            {
                Popup.PlacementTarget = element;
                Popup.Placement = PlacementMode.Right;
                Popup.IsOpen = true;
                Header.PopupText.Text = text;
            }
        }

        // Start: MenuLeft PopupButton //
        private void btnTask1_MouseEnter(object sender, MouseEventArgs e) => ShowPopup(btnTask1, "Зарегистрироваться");
        private void btnTask2_MouseEnter(object sender, MouseEventArgs e) => ShowPopup(btnTask2, "Войти");
        private void btnTask3_MouseEnter(object sender, MouseEventArgs e) => ShowPopup(btnTask3, "TopTalk");
        //private void btnTask4_MouseEnter(object sender, MouseEventArgs e) => ShowPopup(btnTask4, "Task 4");
        //private void btnTask5_MouseEnter(object sender, MouseEventArgs e) => ShowPopup(btnTask5, "Task 5");
        private void btnSetting_MouseEnter(object sender, MouseEventArgs e) => ShowPopup(btnSetting, "Настройки");


        // End: MenuLeft PopupButton //

        // Start: Button Close | Restore | Minimize 
        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // End: Button Close | Restore | Minimize

        private void btnSettings_Click(object sender, RoutedEventArgs e)
            => fContainer.Navigate(new System.Uri("Pages/Settings.xaml", UriKind.RelativeOrAbsolute));

        private void btnTask1_Click(object sender, RoutedEventArgs e)
            => fContainer.Navigate(new System.Uri("Pages/RegisterPage.xaml", UriKind.RelativeOrAbsolute));

        private void btnTask2_Click(object sender, RoutedEventArgs e)
            => fContainer.Navigate(new System.Uri("Pages/LoginPage.xaml", UriKind.RelativeOrAbsolute));

        private void btnTask3_Click(object sender, RoutedEventArgs e)
            => fContainer.Navigate(new System.Uri("Pages/TopTalkClientPage.xaml", UriKind.RelativeOrAbsolute));

        private void btnTask4_Click(object sender, RoutedEventArgs e)
            => fContainer.Navigate(new System.Uri("Pages/Task4.xaml", UriKind.RelativeOrAbsolute));

        private void btnTask5_Click(object sender, RoutedEventArgs e)
            => fContainer.Navigate(new System.Uri("Pages/Task5.xaml", UriKind.RelativeOrAbsolute));

        private void WindowMove(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}