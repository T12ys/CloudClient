using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Text;
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
        using var stream = client.GetStream();
        using var writer = new StreamWriter(stream) { AutoFlush = true };
        using var reader = new StreamReader(stream);
        
        await writer.WriteLineAsync(message);
        
        var sb = new StringBuilder();
        char[] buffer = new char[1024];
        int read;
        while ((read = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            sb.Append(buffer, 0, read);
            if (sb.Length > 0 && sb[sb.Length - 1] == '\n')
                break;
        }

        return sb.ToString().TrimEnd('\n');
    }

}
