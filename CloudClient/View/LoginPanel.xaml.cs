using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using CloudClient.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CloudClient.Model;
using CloudClient.Services;


namespace CloudClient.Veiw;

public partial class LoginPanel : Window
{
    public LoginPanel()
    {
        InitializeComponent();
        var explorer = new FileExplorer(); 
        var vm = new LoginViewModel(explorer);
        this.DataContext = vm;
        vm.OnMessageBox += message => MessageBox.Show(message);

        vm.RequestClose += () => this.Close();
    }
    
    private void LoginPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel vm)
        {
            vm.PasswordLogin = ((PasswordBox)sender).Password;
        }
    }
    
    private void GoToRegister(object sender, MouseButtonEventArgs e)
    {
        RegisterPanel registerPanel = new RegisterPanel();
        registerPanel.Show();
        this.Close();  
    }
}