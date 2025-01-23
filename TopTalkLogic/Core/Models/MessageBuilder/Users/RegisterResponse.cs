
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

namespace TopTalk.Core.Models.MessageBuilder.Users;

public class RegisterResponseData : IMsgSourceData
{
    public string Payload { get; set; } = string.Empty;

    public string MessageType => MsgType;
    public static string MsgType => "RegisterResponse";
}

public class RegisterResponse : IMessageBuilder<RegisterResponseData>
{
    private RegisterResponseData _data = new();

    public RegisterResponse SetExplanatoryMsg(string payload)
    {
        _data.Payload = payload;
        return this;
    }

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
            Payload = _data.Payload
        };
    }

    public static RegisterResponseData Parse(Message msg)
    {
        if (msg.MessageType != RegisterResponseData.MsgType)
            throw new InvalidOperationException("Incorrect message type.");

        return new RegisterResponseData()
        {
            Payload = msg.Payload,
        };
    }
}