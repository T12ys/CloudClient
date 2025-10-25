namespace CloudClient.Requests;

public class CopyRequest: BaseRequest
{
    public string SelectedFilePath { get; set; }
    
    public CopyRequest() : base("COPY") {}
}