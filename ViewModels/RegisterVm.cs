
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
            RegisterCommand = new RelayCommand(_ => Register(), _ => CanRegister());
        }

        private void Register()
        {
            // Логика регистрации
        }

        private bool CanRegister()
        {
            return !string.IsNullOrEmpty(Login)
                && !string.IsNullOrEmpty(Password)
                && Password == ConfirmPassword;
        }
    }
}
