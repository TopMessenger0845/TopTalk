using TopNetwork.Services;
using TopTalk.Core.Storage.Models;
using TopTalk.Core.Storage.DataBaseInteract;
using Microsoft.EntityFrameworkCore;

namespace TopTalk.Core.Services.Builders
{
    public class UserBuilderService
    {
        public IPasswordService passwordService;
        public UserBuilderService()
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
