﻿
using System.Windows.Controls;
using System.Windows.Input;
using TopTalk.ViewModels;

namespace TopTalk.Pages
{
    /// <summary>
    /// Логика взаимодействия для TopTalkClientPage.xaml
    /// </summary>
    public partial class TopTalkClientPage : Page
    {
        public static TopTalkClientVm Instance { get; set; } = new();

        public TopTalkClientPage()
        {
            InitializeComponent();
            Instance.OnUpdateMessages += ScrollToEnd;
            DataContext = Instance;
        }

        private void HintsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is string selectedHint)
            {
                var viewModel = DataContext as HintOnMessageInputBoxBaseVm;
                int index = selectedHint.IndexOf(' ');
                index = index == -1 ? selectedHint.Length : index;
                viewModel?.SelectHint(selectedHint[..index]);
                listBox.SelectedItem = null; // Сбрасываем выбор
                InputTextBox.Focus();
                CareInputTextBoxToEnd();
            }
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (DataContext is ClientViewModel vm && vm.SendMessageCommand.CanExecute(null))
                {
                    vm.SendMessageCommand.Execute(null);
                    vm.Hints.Clear();
                    vm.AreHintsVisible = vm.Hints.Any();
                }
                e.Handled = true;
            }
            if (e.Key == Key.Tab)
            {
                if (DataContext is HintOnMessageInputBoxBaseVm vm)
                {
                    int index = InputTextBox.Text.LastIndexOf('/');
                    if (index == -1) return;
                    string text = InputTextBox.Text[index..];
                    text = vm.TryCompleteCommand(text);
                    InputTextBox.Text = InputTextBox.Text[..index] + text;
                    CareInputTextBoxToEnd();
                    e.Handled = true;
                }
                e.Handled = true;
            }
        }
        // Переместить каретку в конец
        public void CareInputTextBoxToEnd()
            => InputTextBox.CaretIndex = InputTextBox.Text.Length;

        private void ScrollToEnd()
        {
            MessagesListBox.ScrollIntoView(MessagesListBox.Items[^1]);
        }
    }
}
