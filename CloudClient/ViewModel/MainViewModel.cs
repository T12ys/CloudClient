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

    public event Action<string> OnMessageBox;
    public ObservableCollection<FileNode> CurrentItems => explorer.CurrentItems;

    
   
    public FileNode SelectedItem
    {
        get => explorer.SelectedItem;
        set => explorer.SelectedItem = value;
    }

   
    public IRelayCommand NavigateBackCommand { get; }
    public IRelayCommand OpenSelectedItemCommand { get; }
    public IRelayCommand CreateFolderButton { get; }
    
    
    public IRelayCommand UploadFileCommand { get; }
    public IRelayCommand DownloadFileCommand { get; }
    public IRelayCommand RenameCommand { get; }
    public IRelayCommand DeleteCommand { get; }
    public IRelayCommand CopyCommand { get; }
    public IRelayCommand ExitCommand { get; }
    
    
    
    public MainViewModel(string currentUsername ,FileExplorer explorer)
    {
        authService = new AuthService();
        this.explorer = explorer;
       
        

        NavigateBackCommand = new RelayCommand(() => { explorer.NavigateBack(); OnPropertyChanged(nameof(CurrentPath)); });
        OpenSelectedItemCommand = new RelayCommand(() => { explorer.OpenSelectedItem(); OnPropertyChanged(nameof(CurrentPath)); });

        
        CreateFolderButton = new RelayCommand(() =>
        {
            CreateFolderPanel Panel = new CreateFolderPanel(currentUsername , explorer);
            Panel.ShowDialog();
        });
        
        UploadFileCommand = new RelayCommand(async () =>
        {
            await UploadFile();
        });
        
        DownloadFileCommand = new RelayCommand(async () =>
        {
            await DownloadFile();
        });
        RenameCommand = new RelayCommand(() =>
        {
            RenamePanel Panel = new RenamePanel(explorer);
            Panel.ShowDialog();
        });
        DeleteCommand = new RelayCommand(async() =>
        {
            await Delete();
        });
        CopyCommand = new RelayCommand(async() =>
        {
            await Copy();
        });
        ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        
    }
    

  

    
    
    //=================================================================


    private async Task UploadFile()
    {
        Console.WriteLine("Метод добавления файла запушен");

        Response<string> response = await authService.UploadFileAsync(CurrentPath);
        MessageBox.Show($"{response.Message}");
        if (response.Success)
        {
            FileNode rootNode = JsonSerializer.Deserialize<FileNode>(response.Data);
            
            explorer.LoadRoot(rootNode);
            MyServerHelper.PrintTree(rootNode, 0);
        }
    }

    private async Task DownloadFile()
    {
        Console.WriteLine("Метод скачивания файла запушен");
        Console.WriteLine($"{SelectedItem?.FullPath}");
        Response<string> response = await authService.DownloadFileAsync(SelectedItem?.FullPath);
        
        
        OnMessageBox?.Invoke(response.Message);
        
        
    }
    
    private async Task Delete()
    {
        Console.WriteLine("Метод удаления запушен");
        Console.WriteLine($"{SelectedItem?.FullPath}");
        Response<string> response = await authService.DeleteAsync(SelectedItem?.FullPath);
        
        OnMessageBox?.Invoke(response.Message);
        
        
        if (response.Success)
        {
            FileNode rootNode = JsonSerializer.Deserialize<FileNode>(response.Data);
            
            explorer.LoadRoot(rootNode);
            MyServerHelper.PrintTree(rootNode, 0);
        }
        
    }
    
    private async Task Copy()
    {
        Console.WriteLine("Метод копирования запушен");
        Console.WriteLine($"{SelectedItem?.FullPath}");
        Response<string> response = await authService.CopyAsync(SelectedItem?.FullPath);
        
        
        OnMessageBox?.Invoke(response.Message);
        
        
        if (response.Success)
        {
            FileNode rootNode = JsonSerializer.Deserialize<FileNode>(response.Data);
            
            explorer.LoadRoot(rootNode);
            MyServerHelper.PrintTree(rootNode, 0);
        }
        
    }
    
    public string CurrentPath
    {
        get => explorer.CurrentPath;
    }

}

