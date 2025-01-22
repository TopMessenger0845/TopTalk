
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
        public DbSet<ContactGroupEntity> Contacts { get; set; }
        public MainContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TopTalkChat;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //пользователи и контакты один ко многим
            modelBuilder.Entity<ContactGroupEntity>()
                .HasOne(cg => cg.User)
                .WithMany(u => u.ContactGroups)
                .HasForeignKey(cg => cg);
            //пользователи и сообщения один ко многим
            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            //чаты и сообщения один ко многим
            modelBuilder.Entity<MessageEntity>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);
            //задаем ключи для связи многие ко многим
            modelBuilder.Entity<UserChatEntity>()
                .HasKey(uc => new { uc.UserId, uc.ChatId });
            //делаем связь многие ко многим
            modelBuilder.Entity<UserChatEntity>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserChatEntity>()
                .HasOne(uc => uc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.ChatId);
        }
    }
}
