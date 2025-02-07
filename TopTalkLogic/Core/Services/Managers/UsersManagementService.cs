﻿using TopTalk.Core.Storage.DataBaseInteract;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Services.Managers
{
    /// <summary>
    /// Класс для работы с таблицей пользователей, добавление, получение пользователей и тд.
    /// </summary>
    public class UsersManagementService : EntitiesManagementService<UserEntity>
    {
        private object locker;
        public UsersManagementService()
        {
            locker = new object();
        }
        public override void Add(UserEntity entity)
        {
            lock (locker)
            {
                using (var db = new MainContext())
                {
                    db.Users.Add(entity);
                    db.SaveChanges();
                }
            }
        }
        public override UserEntity Get(int id)
        {
            UserEntity user = null;
            using (var db = new MainContext())
            {
                user = db.Users
                    .Where(user => user.Id == id)
                    .FirstOrDefault()!;
            }
            return user;
        }
        public override ICollection<UserEntity> GetAll()
        {
            ICollection<UserEntity> users = null;
            using (var db = new MainContext())
            {
                users = db.Users.ToList();
            }
            return users;
        }
        public override void Remove(UserEntity entity)
        {
            lock (locker)
            {
                using (var db = new MainContext())
                {
                    db.Users.Remove(entity);
                    db.SaveChanges();
                }
            }
        }
    }
}
