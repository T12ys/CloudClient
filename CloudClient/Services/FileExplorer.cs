using System.Collections.ObjectModel;
using CloudClient.Model;
using System.IO;
using System.Collections.Generic;

namespace CloudClient.Services;


public class FileExplorer
{
    private FileNode rootNode; 
    private FileNode? currentNode; 
    public ObservableCollection<FileNode> CurrentItems { get; set; } = new();
    public FileNode? SelectedItem { get; set; } 
    public string CurrentPath
    {
        get
        {
            if (currentNode != null)
                return currentNode.FullPath;
            else
                return "";
        }
    }

    public void LoadRoot(FileNode root)
    {
        rootNode = root;
        currentNode = rootNode;
        Refresh();
    }
    
    public void OpenSelectedItem()
    {
        if (SelectedItem == null || !SelectedItem.IsDirectory)
        {
            return;
        }

        currentNode = SelectedItem;
        Refresh();
    }
    
    public void NavigateBack()
    {
        if (currentNode == rootNode)
        {
            return;
        }
        currentNode = FindParent(rootNode, currentNode);
        Refresh();
    }
    
    private void Refresh()
    {
        CurrentItems.Clear();
        if (currentNode?.Children == null)
        {
            return;
        }

        foreach (var child in currentNode.Children)
        {
            CurrentItems.Add(child);
        }
    }
    
    private FileNode FindParent(FileNode parent, FileNode target)
    {
        if (parent.Children == null)
        {
            return null;
        }

        foreach (var child in parent.Children)
        {
            if (child == target)
            {
                return parent;
            }

            var result = FindParent(child, target);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}
