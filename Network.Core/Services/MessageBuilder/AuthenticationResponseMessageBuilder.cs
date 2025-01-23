using TopNetwork.Core;

namespace TopNetwork.Services.MessageBuilder
{
    public class AuthenticationResponseData : IMsgSourceData
    {
        public string Payload { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; } = false;
        public string Login {  get; set; } = string.Empty;

        public string MessageType => MsgType;
        public static string MsgType => "AuthenticationResponse";
    }

    public class AuthenticationResponseMessageBuilder : IMessageBuilder<AuthenticationResponseData>
    {
        private AuthenticationResponseData _data = new();

        public AuthenticationResponseMessageBuilder SetAuthentication(bool authentication)
        {
            _data.IsAuthenticated = authentication;
            return this;
        }
        public AuthenticationResponseMessageBuilder SetExplanatoryMsg(string payload)
        {
            _data.Payload = payload;
            return this;
        }

        public AuthenticationResponseMessageBuilder SetLogin(string login)
        {
            _data.Login = login;
            return this;
        }

        public Message BuildMsg()
        {
            return new()
            {
                MessageType = _data.MessageType,
                Headers = 
                { 
                    { "IsAuthed", _data.IsAuthenticated!.ToString()! },
                    { "Login", _data.Login }
                },
                Payload = _data.Payload
            };
        }

        public static AuthenticationResponseData Parse(Message msg)
        {
            if (msg.MessageType != AuthenticationResponseData.MsgType)
                throw new InvalidOperationException("Incorrect message type.");

            return new()
            {
                IsAuthenticated = bool.Parse(msg.Headers["IsAuthed"]),
                Payload = msg.Payload,
                Login = msg.Headers["Login"]
            };
        }
    }
}
