
using System.Windows.Input;

namespace TopTalk.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private MainViewModel _mainModel;

        public ICommand LoginCommand { get; }
        public ICommand SwitchToRegisterCommand { get; }

        public LoginViewModel(MainViewModel mainModel)
        {
            LoginCommand = new RelayCommand(_ => ExecuteLogin());
            SwitchToRegisterCommand = new RelayCommand(_ => mainModel.SetRegisterView());
            _mainModel = mainModel;
        }

        private void ExecuteLogin()
        {
            // Логика входа
        }
    }
}
