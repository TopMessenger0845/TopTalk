
using System.Windows.Input;

namespace TopTalk.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        private LoginViewModel? _loginViewModel;
        private RegisterViewModel? _registerViewModel;

        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand CloseCommand { get; }
        public ICommand SwitchToLoginCommand { get; }
        public ICommand SwitchToRegisterCommand { get; }

        public MainViewModel()
        {
            //CloseCommand = new RelayCommand(_ => System.Windows.Application.Current.Shutdown());
            SwitchToLoginCommand = new RelayCommand(_ => SetLoginView());
            SwitchToRegisterCommand = new RelayCommand(_ => SetRegisterView());

            // Начальная страница
            SetLoginView();
        }

        public void SetRegisterView()
        {
            _registerViewModel ??= new RegisterViewModel(this);
            _currentView = _registerViewModel;
        }

        public void SetLoginView()
        {
            _loginViewModel ??= new LoginViewModel(this);
            _currentView = _loginViewModel;
        }
    }
}
