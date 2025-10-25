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

public class RegisterViewModel: ObservableObject
{
    
    private AuthService authService;
    
    public IRelayCommand Register { get; }

    public RegisterViewModel()
    {
        authService = new AuthService();
        
        
        Register = new RelayCommand(async () => await RegisterCommand());
        
    }
    
    private async Task RegisterCommand()
    {
        Console.WriteLine("Метод регистрации запушен");
        Response<string> response = await authService.RegistrationAsync(UsernameRegistr, PasswordRegistr, PasswordCopy);
        
        //=============================================================
        //=============================================================
        MessageBox.Show($"{response.Message}");
        //=============================================================
        //=============================================================
        
    }
    
    private string _passwordRegistr;
    public string PasswordRegistr
    {
        get => _passwordRegistr;
        set => SetProperty(ref _passwordRegistr, value);
    }

    
    
    private string _passwordCopy;
    public string PasswordCopy
    {
        get => _passwordCopy;
        set => SetProperty(ref _passwordCopy, value);
    }
    
    private string _usernameRegistr;
    public string UsernameRegistr
    {
        get => _usernameRegistr;
        set => SetProperty(ref _usernameRegistr, value);
    }

}