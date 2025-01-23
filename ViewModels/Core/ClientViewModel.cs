
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TopTalkLogic.Core.Models;

namespace TopTalk.ViewModels
{
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Content { get; set; }
    }

    public abstract class ClientViewModel : HintOnMessageInputBoxBaseVm
    {
        protected readonly BaseClient Client;
        public string YouName { get; set; } = "Вы";
        // Чат и сообщения
        public ObservableCollection<ChatMessage> Messages { get; } = [];

        // Свойства
        public ICommand SendMessageCommand { get; }
        public event Action? OnUpdateMessages;

        protected ClientViewModel(BaseClient client)
        {
            Client = client;

            SendMessageCommand = new RelayCommand(async () => await SendMessageAsync(), () => true);

            Client.OnConnectionLost += () => AddMessage("Client", "Соединение потеряно.");
            Client.OnErroreOnClient += error => AddMessage("Client", $"Ошибка: {error}");
            Client.OnErroreFromServer += error => AddMessage("Server", $"Ошибка: [{error.Payload}]");
            Client.OnServerOverloaded += () => AddMessage("ServerResponse", "Сервер перегружен.");
        }

        private async Task SendMessageAsync()
        {
            string input = NewMessage.TrimEnd();
            AddMessage(YouName, NewMessage);
            NewMessage = string.Empty;

            try
            {
                await ExecuteCommandAsync(input);
            }
            catch (Exception ex)
            {
                AddMessage("_commandProcessor", ex.Message);
            }
        }

        protected abstract Task ExecuteCommandAsync(string input);
       
        protected void ShowMessageBox(string message)
            => Application.Current.Dispatcher.Invoke(() => MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information));

        protected void AddMessage(string sender, string message)
            => Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(new ChatMessage { Sender = sender, Content = message });
                OnUpdateMessages?.Invoke();
            });

        private bool CanSendMessage() => !string.IsNullOrWhiteSpace(NewMessage);
    }
}
