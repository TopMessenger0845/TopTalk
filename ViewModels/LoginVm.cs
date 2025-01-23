
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
            LoginCommand = new RelayCommand(_ => TryLogin(), _ => CanLogin());
        }

        private void TryLogin()
        {
            
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }
    }

}
