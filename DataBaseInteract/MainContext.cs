using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using TopTalk.Models;
namespace TopTalk.DataBaseInteract
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
            modelBuilder.Entity<Chat>().HasMany(u => u.Users).WithMany(u => u.Chats);
            modelBuilder.Entity<Message>().HasOne(u => u.Chat).WithMany(u => u.Messages).HasForeignKey(u=>u.ChatId);
            modelBuilder.Entity<User>().HasOne(u => u.Contacts).WithMany(u=>u.Contacts); // чето кажется я не так связь сделал :/
        }
    }
}
