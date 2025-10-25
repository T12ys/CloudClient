namespace CloudClient.Requests;

public class DeleteRequest: BaseRequest
{
    
    public string SelectedFilePath { get; set; }
    
    public DeleteRequest() : base("DELETE") {}
}