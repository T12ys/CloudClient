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
public partial class MainWindow : Window
{
    public MainWindow(string currentUsername , FileExplorer explorer)
    {
        InitializeComponent();
        var a = new MainViewModel( currentUsername ,explorer);
        DataContext = a;
        a.OnMessageBox += message => MessageBox.Show(message);
    }
    
    private void FileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is MainViewModel vm)
            vm.OpenSelectedItemCommand.Execute(null);
    }
    
}