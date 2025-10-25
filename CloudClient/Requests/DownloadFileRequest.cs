namespace CloudClient.Requests;

public class DownloadFileRequest: BaseRequest
{
    public string SelectedFilePath { get; set; }
    public bool FileExistsIndicator { get; set; }
    public int FileIncrement { get; set; }
    public DownloadFileRequest() : base("DOWNLOADFILE") {}
}