﻿
using System.Data.Entity;
using TopNetwork.Services;
using TopTalk.Core.Storage.DataBaseInteract;
using TopTalk.Core.Storage.Enums;
using TopTalk.Core.Storage.Models;

namespace TopTalkLogic.Core.Services
{
    public class DbService
    {
        private static SemaphoreSlim _semaphore = new(1, 1);

        public async Task<List<MessageEntity>> GetMessagesByChatAsync(Guid chatId)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();
                return await context.Messages.Where(msg => msg.ChatId == chatId).ToListAsync();
            }
            finally { _semaphore.Release(); }
        }

        public async Task<List<ChatEntity>> GetChatsByUser(Guid userId)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();
                return await context.UsersAndChats.Where(chatUser => chatUser.UserId == userId).Select(chat => chat.Chat).ToListAsync();
            }
            finally { _semaphore.Release(); }
        }

        public async Task AddMessage(string content, Guid userId, Guid chatId)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();

                var message = new MessageEntity()
                {
                    ChatId = chatId,
                    SenderId = userId,
                    Timestamp = DateTime.UtcNow,
                };
                
                await context.Messages.AddAsync(message);
                await context.SaveChangesAsync();
            }
            finally { _semaphore.Release(); }
        }

        public async Task<Guid> RegisterNewChat(string title, TypesOfChats type, Guid ownerId)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();

                var chat = new ChatEntity()
                {
                    ChatType = type,
                    Name = title,
                };

                await context.Chats.AddAsync(chat);
                await context.SaveChangesAsync();

                var ownerEntity = new UserChatEntity()
                {
                    ChatId = chat.Id,
                    UserId = ownerId,
                    UserType = TypesOfUsers.Owner,
                };

                await context.UsersAndChats.AddAsync(ownerEntity);
                await context.SaveChangesAsync();

                return chat.Id;
            }
            finally { _semaphore.Release(); }
        }

        public async Task AddNewUserToChat(Guid chatId, Guid userId)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();

                var chat = new UserChatEntity()
                {
                    ChatId = chatId,
                    UserId = userId,
                    UserType = TypesOfUsers.Member,
                };

                await context.UsersAndChats.AddAsync(chat);
                await context.SaveChangesAsync();
            }
            finally { _semaphore.Release(); }
        }

        public async Task<bool> IsFreeLogin(string login)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();

                return !context.Users.Any(x => x.Login == login);
            }
            finally { _semaphore.Release(); }
        }

        public async Task RegisterUser(string login, string passwordHash)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();

                var user = new UserEntity()
                { 
                    Login = login,
                    PasswordHash = passwordHash,
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            finally { _semaphore.Release(); }
        }

        public async Task<UserEntity?> GetUserByLogin(string login)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();

                return await context.Users.FirstOrDefaultAsync(x => x.Login == login);
            }
            finally { _semaphore.Release(); }
        }

        public async Task<List<UserChatEntity>> GetAllUserByChat(Guid chatId)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = new MainContext();

                return await context.UsersAndChats.Where(chatUser => chatUser.ChatId == chatId).ToListAsync();
            }
            finally { _semaphore.Release(); }
        }
    }
}
