
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Storage.DataBaseInteract
{
    public static class SqlRequests//хз как у нас будет строиться работа с базой, накидаю наброски условные
    {
        public static bool AuthorizeUser(string login, string password)
        {
            using (var db = new MainContext())
            {
                int passwordHash = password.GetHashCode();//Сделать хэширование пароля
                var users = db.Users.Where(u => u.Login == login && u.PasswordHash == passwordHash).Select(u => u);
                if (users.Count() == 0)
                    return false;
                else
                    return true;
            }
        }
        public static bool RegisterUser(string login, string password)
        {
            using (var db = new MainContext())
            {
                int passwordHash = password.GetHashCode();//Сделать хэширование пароля
                User user = new User()
                {
                    Login = login,
                    PasswordHash = passwordHash,
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


    }
}
