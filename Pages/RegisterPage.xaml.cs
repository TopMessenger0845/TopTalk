
using System.Windows.Controls;
using TopTalk.ViewModels;

namespace TopTalk.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public static RegisterVm Instance { get; set; } = new();

        public RegisterPage()
        {
            InitializeComponent();
            DataContext = Instance;
        }
    }
}
