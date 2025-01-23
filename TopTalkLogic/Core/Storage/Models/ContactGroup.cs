
namespace TopTalk.Core.Storage.Models
{
    public class ContactGroupEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<UserEntity> Contacts { get; set; }
    }
}
