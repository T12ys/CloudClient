using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using CloudClient.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CloudClient.Requests;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

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

    public async Task<Response<string>> RegistrationAsync(string username, string password, string passwordRepeat)
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

    public async Task<Response<string>> CreateFolderAsync(string username, string password, string folderName,
        string targetDirectory)
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

    public async Task<Response<string>> UploadFileAsync(string targetDirectory)
    {
        var dialog = new OpenFileDialog();
        bool? result = dialog.ShowDialog();
        if (result != true)
            return new Response<string> { Success = false, Message = "Файл не выбран" };

        string filePath = dialog.FileName;
        byte[] fileBytes = await File.ReadAllBytesAsync(filePath);

        var request = new FileUploadRequest
        {
            FileName = Path.GetFileName(filePath),
            TargetDirectory = targetDirectory,
            FileData = fileBytes
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

    public async Task<Response<string>> DownloadFileAsync(string selectedFilePath)
    {
        var dialog = new VistaFolderBrowserDialog
        {
            Description = "Выберите папку"
        };

        string destinationFolderPath;
        if (dialog.ShowDialog() == true)
        {
            destinationFolderPath = dialog.SelectedPath;
        }
        else
        {
            return new Response<string> { Success = false, Message = "Папка не выбрана" };
        }


        var request = new DownloadFileRequest
        {
            SelectedFilePath = selectedFilePath
        };

        string json = JsonSerializer.Serialize(request);
        string responseJson = await _tcp.SendRequestAsync(json);
        Console.WriteLine(responseJson); 

        if (string.IsNullOrEmpty(responseJson))
        {
            return new Response<string> { Success = false, Message = "Не удалось подключиться к серверу" };
        }


        var response = JsonSerializer.Deserialize<Response<FileDataResponse>>(responseJson);

        if (response == null || !response.Success || response.Data == null)
        {
            return new Response<string>
                { Success = false, Message = response?.Message ?? "Ошибка при получении файла" };
        }


        string fullFilePath = Path.Combine(destinationFolderPath, response.Data.FileName);

        try
        {
            await File.WriteAllBytesAsync(fullFilePath, response.Data.FileData);
        }
        catch (Exception ex)
        {
            return new Response<string> { Success = false, Message = $"Не удалось сохранить файл: {ex.Message}" };
        }

        return new Response<string> { Success = true, Message = "Файл успешно загружен", Data = fullFilePath };
    }

    public async Task<Response<string>> RenameAsync(string selectedFilePath ,string newName)
    {
        var request = new RenameRequest
        {
            NewName = newName,
            SelectedFilePath = selectedFilePath
        };
        string json = JsonSerializer.Serialize(request);
        string responseJson = await _tcp.SendRequestAsync(json);
        
        if (string.IsNullOrEmpty(responseJson))
        {
            return new Response<string> { Success = false, Message = "Не удалось подключиться к серверу" };
        }
        
        var response = JsonSerializer.Deserialize<Response<string>>(responseJson);

        if (response == null || !response.Success || response.Data == null)
        {
            return new Response<string>
                { Success = false, Message = response?.Message ?? "Ошибка при получении файла" };
        }

        return response;
    }
    
    
    public async Task<Response<string>> DeleteAsync(string selectedFilePath )
    {
        var request = new DeleteRequest
        {
            SelectedFilePath = selectedFilePath
        };
        string json = JsonSerializer.Serialize(request);
        string responseJson = await _tcp.SendRequestAsync(json);
        
        if (string.IsNullOrEmpty(responseJson))
        {
            return new Response<string> { Success = false, Message = "Не удалось подключиться к серверу" };
        }
        
        var response = JsonSerializer.Deserialize<Response<string>>(responseJson);

        if (response == null || !response.Success || response.Data == null)
        {
            return new Response<string>
                { Success = false, Message = response?.Message ?? "Ошибка при получении файла" };
        }

        return response;
    }
    
    public async Task<Response<string>> CopyAsync(string selectedFilePath )
    {
        var request = new CopyRequest
        {
            SelectedFilePath = selectedFilePath
        };
        string json = JsonSerializer.Serialize(request);
        string responseJson = await _tcp.SendRequestAsync(json);
        
        if (string.IsNullOrEmpty(responseJson))
        {
            return new Response<string> { Success = false, Message = "Не удалось подключиться к серверу" };
        }
        
        var response = JsonSerializer.Deserialize<Response<string>>(responseJson);

        if (response == null || !response.Success || response.Data == null)
        {
            return new Response<string>
                { Success = false, Message = response?.Message ?? "Ошибка при получении файла" };
        }

        return response;
    }
    
}