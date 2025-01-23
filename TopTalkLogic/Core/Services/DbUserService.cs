
using TopNetwork.Core;
using TopNetwork.Services;
using TopTalk.Core.Storage.Models;

namespace TopTalkLogic.Core.Services
{
    public class DbUserService
    {
        [Inject]
        public DbService DbService { get; set; }

        [Inject]
        public PasswordService PasswordService { get; set; }

        public async Task RegisterUser(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Логин и пароль не могут быть пустыми.");

            if (await DbService.IsFreeLogin(login) == false)
                throw new InvalidOperationException("Пользователь с таким логином уже существует.");

            await DbService.RegisterUser(login, password);
        }

        public async Task<UserEntity?> Authenticate(string login, string password)
        {
            var user = await DbService.GetUserByLogin(login);

            if (user != null && PasswordService.VerifyHashedPassword(user.PasswordHash, password))
            {
                return user;
            }

            return null;
        }
    }
}
