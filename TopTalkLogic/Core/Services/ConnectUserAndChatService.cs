using TopTalk.Core.Storage.Models;
using TopTalk.Core.Storage.DataBaseInteract;

namespace TopTalk.Core.Services
{
    /// <summary>
    /// Класс для связи пользователя и чата в базе данных
    /// </summary>
    public class ConnectUserAndChatService
    {
        private object locker;
        public ConnectUserAndChatService()
        {
            locker = new object();
        }
        public void Connect(UserEntity user, ChatEntity chat)
        {
            lock (locker)//хз нужно ли тут удерживать поток если честно
            {
                using (var db = new MainContext())
                {
                    db.UsersAndChats.Add(new UserChatEntity()
                    {
                        User = user,
                        UserId = user.Id,
                        Chat = chat,
                        ChatId = chat.Id,
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
