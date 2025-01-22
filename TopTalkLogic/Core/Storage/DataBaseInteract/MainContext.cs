
using Microsoft.EntityFrameworkCore;
using TopTalk.Core.Storage.Models;

namespace TopTalk.Core.Storage.DataBaseInteract
{
    public class MainContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User> UsersAndChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ContactGroup> Contacts { get; set; }
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
            modelBuilder.Entity<ContactGroup>()
                .HasOne(cg => cg.User)
                .WithMany(u => u.ContactGroups)
                .HasForeignKey(cg => cg);
            //пользователи и сообщения один ко многим
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            //чаты и сообщения один ко многим
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);
            //задаем ключи для связи многие ко многим
            modelBuilder.Entity<UserChat>()
                .HasKey(uc => new { uc.UserId, uc.ChatId });
            //делаем связь многие ко многим
            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.ChatId);
        }
    }
}
