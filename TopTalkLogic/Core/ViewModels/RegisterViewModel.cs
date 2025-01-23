
using System.Windows.Input;

namespace TopTalk.Core.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; }
        public ICommand SwitchToLoginCommand { get; }

        public RegisterViewModel(MainViewModel mainView)
        {
            RegisterCommand = new RelayCommand(_ => ExecuteRegister());
            SwitchToLoginCommand = new RelayCommand(_ => mainView.SetLoginView());
        }

        private void ExecuteRegister()
        {
            // Логика регистрации
        }
    }
}
