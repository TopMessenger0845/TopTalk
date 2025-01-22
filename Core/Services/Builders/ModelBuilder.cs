using TopTalk.Core.Storage.Enums;

namespace TopTalk.Core.Services.Builders
{
    public abstract class ModelBuilder<T> where T : class //не используется (пока что), хотел чтобы его реализовывали наследники билдеры, но хз как реализовать
    {
        public abstract T Build();
    }
}
