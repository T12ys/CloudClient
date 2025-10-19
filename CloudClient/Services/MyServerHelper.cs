using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using CloudClient.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CloudClient.Requests;



namespace CloudClient.Services;

public class MyServerHelper
{
    public static void PrintTree(FileNode node, int indent)
    {
        if (node == null) return;

        Console.WriteLine($"{new string(' ', indent * 2)}- {node.Name}");

        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                PrintTree(child, indent + 1);
            }
        }
    }
    
    
}

