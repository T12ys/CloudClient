namespace CloudClient.Requests;

public class RenameRequest: BaseRequest
{
    public string NewName{ get; set; }
    public string SelectedFilePath { get; set; }
    
    public RenameRequest() : base("RENAME") {}
}