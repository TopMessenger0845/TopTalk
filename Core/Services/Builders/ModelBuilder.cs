using TopTalk.Core.Storage.Enums;

namespace TopTalk.Core.Services.Builders
{
    public abstract class ModelBuilder<T> where T : class //не используется (пока что)
    {
        public abstract T Build(T entity, string passwordHash);
        public abstract T Build(T entity, string chatName, TypesOfChats chatType);
    }
}
