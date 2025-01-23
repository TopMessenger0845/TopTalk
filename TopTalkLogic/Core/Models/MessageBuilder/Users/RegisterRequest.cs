
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Users;

public class RegisterRequestData : IMsgSourceData
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string MessageType => MsgType;
    public static string MsgType => "RegisterRequest";
}

public class RegisterRequest : IMessageBuilder<RegisterRequestData>
{
    private RegisterRequestData _data = new();

    public RegisterRequest SetLogin(string login)
    {
        _data.Login = login;
        return this;
    }

    public RegisterRequest SetPassword(string password)
    {
        _data.Password = password;
        return this;
    }

    public Message BuildMsg()
    {
        return new Message()
        {
            Payload = $"{_data.Login}:{_data.Password}",
            MessageType = _data.MessageType,
        };
    }

    public static RegisterRequestData Parse(Message msg)
    {
        if (msg.MessageType != RegisterRequestData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        var parts = msg.Payload.Split(':');
        return new()
        {
            Login = parts[0],
            Password = parts[1],
        };
    }
}