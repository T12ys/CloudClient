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

public partial class CreateFolderPanel: Window
{
    public CreateFolderPanel(string username ,FileExplorer explorer)
    {
        InitializeComponent();
        var vm = new CreateFolderViewModel(username, explorer);
        DataContext = vm;

        vm.RequestClose += () => this.Close();
        vm.OnMessageBox += message => MessageBox.Show(message);
    }
    
    private void CreateFolderPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
{
    if (DataContext is CreateFolderViewModel vm)
    {
        vm.PasswordCreateFolder = ((PasswordBox)sender).Password;
    }
}
}