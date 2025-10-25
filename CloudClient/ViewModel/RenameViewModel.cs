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

public class RenameViewModel: ObservableObject
{
    private AuthService authService;
    
    private FileExplorer explorer ;
    public event Action RequestClose;
    
    public ObservableCollection<FileNode> CurrentItems => explorer.CurrentItems;

    public IRelayCommand Rename { get; }
    public RenameViewModel(FileExplorer explorer)
    {
        authService = new AuthService();
        this.explorer = explorer;

        Rename = new RelayCommand(async () =>
        {
            await RenameCommand();
            RequestClose?.Invoke();
        });
        
    }


    private async Task RenameCommand()
    {
        Console.WriteLine("Метод переименовывания запушен");

        Response<string> response = await authService.RenameAsync(SelectedItem?.FullPath , NewName );
        
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
    
    
    
    public FileNode SelectedItem
    {
        get => explorer.SelectedItem;
        set => explorer.SelectedItem = value;
    }
    
    
    private string _newName;

    public string NewName
    {
        get => _newName;
        set => SetProperty(ref _newName, value);
    }
}