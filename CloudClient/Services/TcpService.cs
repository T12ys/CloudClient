using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public class TcpService
{
    private string _host;
    private int _port;

    public TcpService(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public async Task<string> SendRequestAsync(string message)
    {
        using var client = new TcpClient(_host, _port);
        using var writer = new StreamWriter(client.GetStream());
        using var reader = new StreamReader(client.GetStream());

        await writer.WriteLineAsync(message);
        await writer.FlushAsync();

        string response = await reader.ReadLineAsync();
        return response;
    }
}
