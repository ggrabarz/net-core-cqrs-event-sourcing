using NetCoreCqrs.Api.Core.Domain;

namespace NetCoreCqrs.Api.Core.WriteModel.RepositoryCache
{
    public interface IRepositoryCache<T> where T : AggregateRoot
    {
        T GetOrDefault(string key);
        void Refresh(string key);
        void Remove(string key);
        void Set(string key, T value);
    }
}
