
namespace CloudClient.Requests;
public class LoginRequest : BaseRequest
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginRequest() : base("LOGIN") {}
}