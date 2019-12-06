using NetCoreCqrs.Api.Core.Domain;

namespace NetCoreCqrs.Api.Core.WriteModel.RepositorySnapshotCache
{
    public interface IRepositorySnapshotCache<T> where T : AggregateRoot
    {
        T GetOrDefault(string key);
        void Set(string key, T value);
    }
}
