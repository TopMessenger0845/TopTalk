
using Microsoft.EntityFrameworkCore;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Storage.DataBaseInteract
{
    public class MainContext : DbContext
    {
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserChatEntity> UsersAndChats { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public MainContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TopTalkChat;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи между ChatEntity и MessageEntity
            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка связи между UserEntity и MessageEntity (отправленные сообщения)
            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка связи между UserEntity и UserChatEntity
            modelBuilder.Entity<UserChatEntity>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка связи между ChatEntity и UserChatEntity
            modelBuilder.Entity<UserChatEntity>()
                .HasOne(uc => uc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка индексов и уникальности для некоторых свойств
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<ChatEntity>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
