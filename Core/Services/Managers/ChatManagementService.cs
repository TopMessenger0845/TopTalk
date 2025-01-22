using TopTalk.Core.Storage.Models;
using TopTalk.Core.Storage.DataBaseInteract;

namespace TopTalk.Core.Services.Managers
{
    /// <summary>
    /// Класс для работы с таблице чатов, добавление, получение чатов и тд
    /// </summary>
    public class ChatManagementService : EntitiesManagementService<Chat>
    {
        private object locker;
        public ChatManagementService()
        {
            locker = new object();
        }
        public override void Add(Chat entity)
        {
            lock (locker)
            {
                using (var db = new MainContext())
                {
                    db.Add(entity);
                    db.SaveChanges();
                }
            }
        }
        public override Chat Get(int id)
        {
            Chat chat = null;
            using (var db = new MainContext())
            {
                chat = db.Chats
                    .Where(chat => chat.Id == id)
                    .FirstOrDefault()!;
            }
            return chat;
        }
        public override ICollection<Chat> GetAll()
        {
            ICollection<Chat> chats = null;
            using (var db = new MainContext())
            {
                chats = db.Chats.ToList();
            }
            return chats;
        }
        public override void Remove(Chat entity)
        {
            lock (locker)
            {
                using (var db = new MainContext())
                {
                    db.Chats.Remove(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
