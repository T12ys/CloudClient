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

public class AuthService
{
    private TcpService _tcp;

    public AuthService()
    {
        _tcp = new TcpService("192.168.100.173", 8989);
    }
    
    public async Task<Response<string>> LoginAsync(string username, string password)
    {
        var request = new LoginRequest
        {
            Username = username,
            Password = password
        };

        string json = JsonSerializer.Serialize(request);
        string responseJson = await _tcp.SendRequestAsync(json);

        if (string.IsNullOrEmpty(responseJson))
            return new Response<string> { Success = false, Message = "Не удалось подключиться к серверу" };

        var response = JsonSerializer.Deserialize<Response<string>>(responseJson);
        if (response == null)
            return new Response<string> { Success = false, Message = "Ошибка десериализации ответа сервера" };

        return response;
    }

    public async Task<Response<string>> Registration(string username, string password, string passwordRepeat)
    {
        if (password != passwordRepeat)
            return new Response<string> { Success = false, Message = "Пароли не совпадают" };

        var request = new RegistrationRequest
        {
            Username = username,
            Password = password
        };

        string json = JsonSerializer.Serialize(request);
        string responseJson = await _tcp.SendRequestAsync(json);

        if (string.IsNullOrEmpty(responseJson))
            return new Response<string> { Success = false, Message = "Не удалось подключиться к серверу" };

        var response = JsonSerializer.Deserialize<Response<string>>(responseJson);
        if (response == null)
            return new Response<string> { Success = false, Message = "Ошибка десериализации ответа сервера" };

        return response;
    }

    public async Task<Response<string>> CreateFolder(string username, string password, string folderName, string targetDirectory)
    {
        var request = new CreateFolderRequest
        {
            Username = username,
            Password = password,
            FolderName = folderName,
            CurrentDirectory = targetDirectory
        };
        string json = JsonSerializer.Serialize(request);
        string responseJson = await _tcp.SendRequestAsync(json);
        if (string.IsNullOrEmpty(responseJson))
            return new Response<string> { Success = false, Message = "Не удалось подключиться к серверу" };
        
        var response = JsonSerializer.Deserialize<Response<string>>(responseJson);
        if (response == null)
            return new Response<string> { Success = false, Message = "Ошибка десериализации ответа сервера" };
        return response;
    }

    

}

