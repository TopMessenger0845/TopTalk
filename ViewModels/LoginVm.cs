
using System.Windows;
using System.Windows.Input;
using TopTalkLogic.Core.Models;

namespace TopTalk.ViewModels
{
    public class LoginVm : BaseViewModel
    {
        private readonly TopTalkClient _client;

        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        public LoginVm()
        {
            LoginCommand = new RelayCommand(async () => await TryLogin());
            _client = TopTalkClientVm.GetClient().Result;
            _client.OnAuthentication += Client_OnAuthentication; ;

        }

        private void Client_OnAuthentication(TopNetwork.Services.MessageBuilder.AuthenticationResponseData obj)
        {
            MessageBox.Show(obj.Payload, obj.IsAuthenticated ? "Успешная авторизация" : "Ошибка авторизации", MessageBoxButton.OK, obj.IsAuthenticated ? MessageBoxImage.Information : MessageBoxImage.Error);
        }

        private async Task TryLogin()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                MessageBox.Show("Поле Login не должно быть пустым...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Поле Password не должно быть пустым...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            await _client.Login(Login, Password);
        }
    }
}
