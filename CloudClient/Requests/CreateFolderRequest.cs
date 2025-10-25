namespace CloudClient.Requests;

public class CreateFolderRequest:BaseRequest
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string FolderName { get; set; }
    
    public string CurrentDirectory {get; set;}
    
    public CreateFolderRequest() : base("CREATEFOLDER") {}
    
}