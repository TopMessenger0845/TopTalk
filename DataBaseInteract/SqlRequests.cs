using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopTalk.Enums;
using TopTalk.Models;

namespace TopTalk.DataBaseInteract
{
    public static class SqlRequests//хз как у нас будет строиться работа с базой, накидаю наброски условные
    {
        public static bool AuthorizeUser(string login, string password)
        {
            using(var db = new MainContext())
            {
                int passwordHash = password.GetHashCode();
                var users = db.Users.Where(u => u.Login == login && u.PasswordHash == passwordHash).Select(u => u);
                if (users.Count() == 0)
                    return false;
                else
                    return true;
            }
        }
        public static bool RegisterUser(string login, string password, TypesOfUsers type)
        {
            using (var db = new MainContext())
            {
                int passwordHash = password.GetHashCode();// с хэшом я тоже не до конца вдупляю, мб оно)
                User user = new User()
                {
                    Login = login,
                    PasswordHash = passwordHash,
                    UserType = type
                };
                var users = db.Users.Where(u => u.Login == login && u.PasswordHash == passwordHash).Select(u => u);
                if (users.Count() != 0)
                    return false;
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }
            }
        }
        public static void AddMessage(User fromMsg, string text, Chat chat) 
        { 
            using (MainContext db = new MainContext())
            {
                Message msg = new Message()
                {
                    Chat = chat,
                    ChatId = chat.Id,
                    MessageFrom = fromMsg,
                    Text = text
                };
                db.Messages.Add(msg);
                db.SaveChanges();
            }
        }
        public static void AddMessage(User fromMsg, FileInfo file, Chat chat)//хз как у нас будет обработка и отправка файлов пусть пока так, набросок
        { 
            using (MainContext db = new MainContext())
            {
                Message msg = new Message()
                {
                    Chat = chat,
                    ChatId = chat.Id,
                    MessageFrom = fromMsg,
                };
                db.Messages.Add(msg);
                db.SaveChanges();
            }
        }
        public static void DeleteMessage(int msgId) 
        {
            using(var db = new MainContext())
            {
                Message neededMsg = db.Messages.Where(u => u.Id == msgId).FirstOrDefault()!;
                if (neededMsg != null)
                {
                    db.Messages.Remove(neededMsg);
                    db.SaveChanges();
                }
                else
                    throw new NullReferenceException();
            }
        }
        public static void CreateChat() { }
        public static void DeleteChat() { }


    }
}
