using NetCoreCqrs.Domain.BuildingBlocks;

namespace NetCoreCqrs.Persistence.RepositoryCache
{
    public interface IRepositoryCache<T> where T : AggregateRoot
    {
        T GetOrDefault(string key);
        void Refresh(string key);
        void Remove(string key);
        void Set(string key, T value);
    }
}
