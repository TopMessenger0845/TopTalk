using TopNetwork.Services;

namespace TopTalk.Core.Services.Builders
{
    /// <summary>
    /// Билдер отвечающий за создание объекта User
    /// </summary>
    public class UserBuilder
    {
        public IPasswordService passwordService;
        public UserBuilder()
        {
            passwordService = new PasswordService();
        }
        public Storage.Models.User BuildUser(string password, string login)
        {
            string passwordHash = passwordService.HashPassword(password);
            Storage.Models.User user = new Storage.Models.User()
            {
                PasswordHash = passwordHash,
                Login = login
            };
            return user;
        }
        public bool VerifyUser(Storage.Models.User user, string password)
        {
            bool isVerified = passwordService.VerifyHashedPassword(user.PasswordHash, password);
            return isVerified;
        }
    }

}
