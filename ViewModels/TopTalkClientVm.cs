
using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows;
using TopTalkLogic.Core.Models;

namespace TopTalk.ViewModels
{
    public class TopTalkClientVm
    {
        public static TopTalkClient Client { get; set; }

        public static async Task Init()
        {
            try
            {
                Client = new TopTalkClient();
                Client.OnConnectionLost += Client_OnConnectionLost;
                await Client.ConnectAsync("127.0.0.1", 5335);
                new ToastContentBuilder().AddText("Connected to Server").AddText("Успешное подключение к серверу...").Show();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fatal Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private static void Client_OnConnectionLost()
        {
            MessageBox.Show("Сервер здох", "Соединение потерено", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }
}
