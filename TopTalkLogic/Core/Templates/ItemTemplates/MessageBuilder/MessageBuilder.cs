
using TopNetwork.Core;
using TopNetwork.Services.MessageBuilder;

public class $safeitemname$Data : IMsgSourceData
{
    public string MessageType => MsgType;
    public static string MsgType => "$safeitemname$"; 
}

public class $safeitemname$ : IMessageBuilder<$safeitemname$Data>
{
    private $safeitemname$Data _data = new();

    public Message BuildMsg()
    {
        return new Message()
        {
            MessageType = _data.MessageType,
        };
    }

    public static $safeitemname$Data Parse(Message msg)
    {
        if (msg.MessageType != $safeitemname$Data.MsgType) 
            throw new InvalidOperationException("Incorrect message type.");

        return new $safeitemname$Data()
        {
            // Add properties initialization if needed
        };
    }
}