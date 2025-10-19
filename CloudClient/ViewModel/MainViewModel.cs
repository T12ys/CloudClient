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

        
        CreateFolderButton = new RelayCommand(async () =>
        {
            MessageBox.Show($"{currentUsername}");
            CreateFolderPanel popup = new CreateFolderPanel(currentUsername , explorer);
            popup.ShowDialog();
        });
        
        UploadFileCommand = new RelayCommand(() => { });
        DownloadFileCommand = new RelayCommand(() => { });
        RenameCommand = new RelayCommand(() => { });
        DeleteCommand = new RelayCommand(() => { });
        CopyCommand = new RelayCommand(() => { });
        ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        
    }
    

  

    
    
    //=================================================================
    
  
    public string CurrentPath
    {
        get => explorer.CurrentPath;
    }

}

