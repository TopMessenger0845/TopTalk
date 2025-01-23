
using System.Windows.Controls;
using TopTalk.ViewModels;

namespace TopTalk.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static LoginVm Instance { get; set; } = new();

        public LoginPage()
        {
            InitializeComponent();
            DataContext = Instance;
        }
    }
}
