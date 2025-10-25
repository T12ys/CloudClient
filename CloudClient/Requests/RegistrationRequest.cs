

namespace CloudClient.Requests;

public class RegistrationRequest : BaseRequest
{
   
    public string Username { get; set; }
    public string Password { get; set; }
    public RegistrationRequest() : base("REGISTRATION") {}
}