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

public class LoginViewModel: ObservableObject
{
    private AuthService authService;
    
    private FileExplorer explorer ;
    
    public IRelayCommand Login { get; }
    
    public event Action RequestClose;
    
    public event Action<string> OnMessageBox;

    public LoginViewModel(FileExplorer explorer)
    {
        authService = new AuthService();
        this.explorer = explorer;
        
        Login = new RelayCommand(async () =>
        {
            await LoginCommand();
            
        });
    }
    
    private async Task LoginCommand()
    {
        Console.WriteLine("Метод входа запушен");
        Response<string> response = await authService.LoginAsync(UsernameLogin, PasswordLogin);
        
        
        OnMessageBox?.Invoke(response.Message);
        
        
        if (response.Success)
        {
            CurrentUsername = UsernameLogin;
            FileNode rootNode = JsonSerializer.Deserialize<FileNode>(response.Data);
            
            explorer.LoadRoot(rootNode);
            MyServerHelper.PrintTree(rootNode, 0);
            
            MainWindow mainWindow = new MainWindow(CurrentUsername ,explorer);
            mainWindow.Show();
            
            RequestClose?.Invoke();
        }
        
    }
    
    private string _usernameLogin;
    public string UsernameLogin
    {
        get => _usernameLogin;
        set => SetProperty(ref _usernameLogin, value);
    }
    
    private string _passwordLogin;
    public string PasswordLogin
    {
        get => _passwordLogin;
        set => SetProperty(ref _passwordLogin, value);
    }
    
    private string _currentUsername;
    public string CurrentUsername
    {
        get => _currentUsername;
        private set => SetProperty(ref _currentUsername, value);
    }
}