
using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows;
using TopTalkLogic.Core.Models;

namespace TopTalk.ViewModels
{
    public class TopTalkClientVm : ClientViewModel
    {
        private static SemaphoreSlim _semaphore = new(1, 1);
        public static TopTalkClient Client { get; set; }

        public TopTalkClientVm() : base(GetClient().Result)
        {

        }
        public static async Task Init()
        {
            await _semaphore.WaitAsync();
            try
            {
                Client = new TopTalkClient();
                Client.OnConnectionLost += Client_OnConnectionLost;
                await Client.ConnectAsync("127.0.0.1", 5335);

                Client.OnErroreFromServer += Client_OnErroreFromServer;
                Client.OnErroreOnClient += data => MessageBox.Show(data, "Ошибка на клиенте", MessageBoxButton.OK, MessageBoxImage.Error);

                new ToastContentBuilder().AddText("Connected to Server").AddText("Успешное подключение к серверу...").Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
            finally { _semaphore.Release(); }
        }

        public static async Task<TopTalkClient> GetClient()
        {
            await _semaphore.WaitAsync();
            try { return Client; }
            finally { _semaphore.Release(); }
        }
        protected override Task ExecuteCommandAsync(string input)
        {
            throw new NotImplementedException();
        }

        private static void Client_OnErroreFromServer(TopNetwork.Services.MessageBuilder.ErroreData obj)
        {
            MessageBox.Show(obj.Payload, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static void Client_OnConnectionLost()
        {
            MessageBox.Show("Сервер здох", "Соединение потерено", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}
