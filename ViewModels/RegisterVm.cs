
using System.Windows;
using System.Windows.Input;

namespace TopTalk.ViewModels
{
    public class RegisterVm : BaseViewModel
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

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand RegisterCommand { get; }

        public RegisterVm()
        {
            RegisterCommand = new RelayCommand(async () => await TryRegister());
        }

        private async Task TryRegister()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                MessageBox.Show("Поле Login не должно быть пустым...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                MessageBox.Show("Поля для паролей не должно быть пустыми...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var passw = Password.Trim();
            var passwConf = ConfirmPassword.Trim();

            var log = Login.Trim();

            if (passw != passwConf)
            {
                MessageBox.Show("Пароли не совпадают...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(Password.Length < 8)
            {
                MessageBox.Show("Длина пароля от 8 символов...", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Вся поля валидны. Вот так система видит логин и пароль.\nЛогин: {log}\nPassword: {passw}\n\n Создать аккаунт?", "Регистрация", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) {
                await TopTalkClientVm.Client.Register(log, passw);
            } 
            else {
                MessageBox.Show("Создания аккаунта отменено...", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
