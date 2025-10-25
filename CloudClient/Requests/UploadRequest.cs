namespace CloudClient.Requests;

public class FileUploadRequest : BaseRequest
{
    public string FileName { get; set; }
    public string TargetDirectory { get; set; }
    public byte[] FileData { get; set; }

    public FileUploadRequest() : base("UPLOADFILE") {}
}