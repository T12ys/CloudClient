using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using CloudClient.Model;
using CloudClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using CloudClient.Veiw;


namespace CloudClient.ViewModel;

public class CreateFolderViewModel: ObservableObject
{
    private AuthService authService;
    
    private FileExplorer explorer ;
    
    public ObservableCollection<FileNode> CurrentItems => explorer.CurrentItems;
    
    public IRelayCommand CreateFolderButton { get; }
    
    public IRelayCommand CreateFolder { get; }

    public CreateFolderViewModel()
    {
        CreateFolderButton = new RelayCommand(async () =>
        {
            var window = new CreateFoldercommandPanel
            {
                DataContext = this
            };
            CreateFolderPanel = Visibility.Visible;
            window.ShowDialog(); 
            
        });
        CreateFolder = new RelayCommand(async () =>
        {

            await CreateFolderCommand();
            CreateFoldercommandPanel Chto = new CreateFoldercommandPanel();
            Application.Current.MainWindow = Chto;
            foreach (Window win in Application.Current.Windows)
            {
                if (win != Chto)
                {
                    win.Close();
                    break;
                }
            }
        });
    }
    
    private async Task CreateFolderCommand()
    {
        Console.WriteLine("Метод регистрации запушен");
        Console.WriteLine($"{CurrentUsername} {PasswordCreateFolder}");
        MessageBox.Show($"CurrentUsername = {CurrentUsername}"); 
        Response<string> response = await authService.CreateFolder(CurrentUsername, PasswordCreateFolder,FolderName,CurrentPath);
        
        MessageBox.Show($"{response.Message}");
        if (response.Success)
        {
            FileNode rootNode = JsonSerializer.Deserialize<FileNode>(response.Data);
            
            explorer.LoadRoot(rootNode);
            MyServerHelper.PrintTree(rootNode, 0);
        }
        
    }
    
    private string _passwordCreateFolder;

    public string PasswordCreateFolder
    {
        get => _passwordCreateFolder;
        set => SetProperty(ref _passwordCreateFolder, value);
    }
    
    public string CurrentPath
    {
        get => explorer.CurrentPath;
    }
    
    private string _folderName;

    public string FolderName
    {
        get => _folderName;
        set => SetProperty(ref _folderName, value);
    }
}