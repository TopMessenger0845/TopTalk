
using System.Windows;
using System.Windows.Input;

namespace TopTalk.ViewModels
{
    public class LoginVm : BaseViewModel
    {
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
            TopTalkClientVm.Client.OnRegister += Client_OnRegister;
        }

        private void Client_OnRegister(Core.Models.MessageBuilder.Users.RegisterResponseData obj)
        {
            MessageBox.Show(obj.Payload);
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

            await TopTalkClientVm.Client.Login(Login, Password);
        }
    }
}
