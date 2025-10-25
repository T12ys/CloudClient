namespace CloudClient.Requests;

public class DownloadFileRequest: BaseRequest
{
    public string SelectedFilePath { get; set; }
    
    public DownloadFileRequest() : base("DOWNLOADFILE") {}
}