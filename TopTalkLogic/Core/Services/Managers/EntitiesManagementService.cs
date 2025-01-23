
namespace TopTalk.Core.Services.Managers
{
    //базовый абстрактный класс для управления данными в таблицах базы
    public abstract class EntitiesManagementService<T> where T : class
    {
        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract ICollection<T> GetAll();
        public abstract T Get(Guid id);
    }
}
