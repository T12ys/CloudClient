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

public partial class RegisterPanel : Window
{
    public RegisterPanel()
    {
        InitializeComponent();
        var vm = new RegisterViewModel();
        DataContext = vm;
        vm.OnMessageBox += message => MessageBox.Show(message);
    }
    private void RegistrPasswordCopy_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is RegisterViewModel vm)
        {
            vm.PasswordCopy = ((PasswordBox)sender).Password;
        }
    }
    private void RegistrPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is RegisterViewModel vm)
        {
            vm.PasswordRegistr = ((PasswordBox)sender).Password;
        }
    }
    
    private void GoToLogin(object sender, RoutedEventArgs e)
    {
        LoginPanel loginPanel = new LoginPanel();
        loginPanel.Show();
        this.Close();
    }
}