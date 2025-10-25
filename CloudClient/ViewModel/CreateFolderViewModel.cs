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
    public event Action RequestClose;
    public ObservableCollection<FileNode> CurrentItems => explorer.CurrentItems;
    
    
    
    public IRelayCommand CreateFolder { get; }

    public CreateFolderViewModel(string currentUsername , FileExplorer explorer)
    {
        _currentUsername = currentUsername;
        authService = new AuthService();
        this.explorer = explorer;
        
        
        CreateFolder = new RelayCommand(async () =>
        {

            await CreateFolderCommand();
            
            RequestClose?.Invoke();
        });
    }
    
    private async Task CreateFolderCommand()
    {
        
        Console.WriteLine("Метод регистрации запушен");
        Console.WriteLine($"{CurrentUsername} {PasswordCreateFolder}");
        Response<string> response = await authService.CreateFolderAsync(CurrentUsername, PasswordCreateFolder,FolderName,CurrentPath);
        
        //=============================================================
        //=============================================================
        MessageBox.Show($"{response.Message}");
        //=============================================================
        //=============================================================
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
    
    private string _currentUsername;
    public string CurrentUsername
    {
        get => _currentUsername;
        private set => SetProperty(ref _currentUsername, value);
    }
    
    
}