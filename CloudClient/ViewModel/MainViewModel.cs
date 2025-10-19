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

public class MainViewModel : ObservableObject
{
    private AuthService authService;
    
    private FileExplorer explorer ;
    
    public ObservableCollection<FileNode> CurrentItems => explorer.CurrentItems;

    
    
    public FileNode SelectedItem
    {
        get => explorer.SelectedItem;
        set => explorer.SelectedItem = value;
    }
    
    private string _currentUsername;
    public string CurrentUsername
    {
        get => _currentUsername;
        private set => SetProperty(ref _currentUsername, value);
    }
    
    
    public IRelayCommand Login { get; }
    public IRelayCommand Register { get; }
    
   
    public IRelayCommand NavigateBackCommand { get; }
    public IRelayCommand OpenSelectedItemCommand { get; }
    
    public IRelayCommand UploadFileCommand { get; }
    public IRelayCommand DownloadFileCommand { get; }
    public IRelayCommand RenameCommand { get; }
    public IRelayCommand DeleteCommand { get; }
    public IRelayCommand CopyCommand { get; }
    public IRelayCommand ExitCommand { get; }
    
    public MainViewModel()
    {
        authService = new AuthService();
        explorer = new FileExplorer();
        Login = new RelayCommand(async () =>
        {
            await LoginCommand();
            
            LoginPanel = Visibility.Collapsed;
            RegisterPanel = Visibility.Collapsed;
            ExplorerPanel = Visibility.Visible;
        });
        Register = new RelayCommand(async () => await RegisterCommand());

        NavigateBackCommand = new RelayCommand(() => { explorer.NavigateBack(); OnPropertyChanged(nameof(CurrentPath)); });
        OpenSelectedItemCommand = new RelayCommand(() => { explorer.OpenSelectedItem(); OnPropertyChanged(nameof(CurrentPath)); });

        
        
        UploadFileCommand = new RelayCommand(() => { });
        DownloadFileCommand = new RelayCommand(() => { });
        RenameCommand = new RelayCommand(() => { });
        DeleteCommand = new RelayCommand(() => { });
        CopyCommand = new RelayCommand(() => { });
        ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        
    }

    private async Task LoginCommand()
    {
        Console.WriteLine("Метод входа запушен");
        Response<string> response = await authService.LoginAsync(Username, PasswordLogin);
        MessageBox.Show($"{response.Message}");
        
        if (response.Success)
        {
            CurrentUsername = Username;
            FileNode rootNode = JsonSerializer.Deserialize<FileNode>(response.Data);
            
            explorer.LoadRoot(rootNode);
            MyServerHelper.PrintTree(rootNode, 0);
        }
        
    }

    private async Task RegisterCommand()
    {
        Console.WriteLine("Метод регистрации запушен");
        Response<string> response = await authService.Registration(Username, PasswordRegistr, PasswordCopy);
        MessageBox.Show($"{response.Message}");
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
    
    
    

    
    
    //=================================================================
    private string _username;
    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }
    
    
    private string _passwordLogin;
    public string PasswordLogin
    {
        get => _passwordLogin;
        set => SetProperty(ref _passwordLogin, value);
    }
    
    private string _passwordRegistr;
    public string PasswordRegistr
    {
        get => _passwordRegistr;
        set => SetProperty(ref _passwordRegistr, value);
    }

    private string _passwordCreateFolder;
    //======================================
    public string PasswordCreateFolder
    {
        get => _passwordCreateFolder;
        set => SetProperty(ref _passwordCreateFolder, value);
    }
    //======================================
    private string _passwordCopy;
    public string PasswordCopy
    {
        get => _passwordCopy;
        set => SetProperty(ref _passwordCopy, value);
    }
//==========================
    private string _folderName;

    public string FolderName
    {
        get => _folderName;
        set => SetProperty(ref _folderName, value);
    }
    //==============================
    private Visibility _loginPanel = Visibility.Visible;
    public Visibility LoginPanel
    {
        get => _loginPanel;
        set => SetProperty(ref _loginPanel, value);
    }

    private Visibility _registerPanel = Visibility.Collapsed;
    public Visibility RegisterPanel
    {
        get => _registerPanel;
        set => SetProperty(ref _registerPanel, value);
    }

    private Visibility _explorerPanel = Visibility.Collapsed;
    public Visibility ExplorerPanel
    {
        get => _explorerPanel;
        set => SetProperty(ref _explorerPanel, value);
    }
    
    private Visibility _createFolderPanel = Visibility.Visible;

    public Visibility CreateFolderPanel
    {
        get => _createFolderPanel;
        set => SetProperty(ref _createFolderPanel, value);
    }
    
//=============
    public string CurrentPath
    {
        get => explorer.CurrentPath;
    }
//===============

}

