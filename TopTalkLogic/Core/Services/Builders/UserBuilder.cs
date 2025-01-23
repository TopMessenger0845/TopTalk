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
        public Storage.Models.UserEntity BuildUser(string password, string login)
        {
            string passwordHash = passwordService.HashPassword(password);
            Storage.Models.UserEntity user = new Storage.Models.UserEntity()
            {
                PasswordHash = passwordHash,
                Login = login
            };
            return user;
        }
        public bool VerifyUser(Storage.Models.UserEntity user, string password)
        {
            bool isVerified = passwordService.VerifyHashedPassword(user.PasswordHash, password);
            return isVerified;
        }
    }

}
