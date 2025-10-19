namespace CloudClient.Requests;

public class BaseRequest
{
    public string Command { get; private set; }

    protected BaseRequest(string command)
    {
        Command = command;
    }
}