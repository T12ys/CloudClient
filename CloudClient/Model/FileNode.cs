namespace CloudClient.Model;

public class FileNode
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string FullPath { get; set; }
    public bool IsDirectory { get; set; }
    public List<FileNode> Children { get; set; } = new();
}