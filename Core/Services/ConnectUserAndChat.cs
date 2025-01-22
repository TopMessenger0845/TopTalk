using TopTalk.Core.Storage.Models;
using TopTalk.Core.Storage.DataBaseInteract;

namespace TopTalk.Core.Services
{
    /// <summary>
    /// Класс для связи пользователя и чата в базе данных
    /// </summary>
    public class ConnectUserAndChat
    {
        private object locker;
        public void Connect(User user, Chat chat)
        {
            lock (locker)//хз нужно ли тут удерживать поток если честно
            {
                using (var db = new MainContext())
                {
                    db.UsersAndChats.Add(new UserChat()
                    {
                        User = user,
                        UserId = user.Id,
                        Chat = chat,
                        ChatId = chat.Id,
                    });
                }
            }
        }
    }
}
