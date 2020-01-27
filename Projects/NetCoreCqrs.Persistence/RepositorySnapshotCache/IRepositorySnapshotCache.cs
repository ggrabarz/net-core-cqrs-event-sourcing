using NetCoreCqrs.Domain.BuildingBlocks;

namespace NetCoreCqrs.Persistence.RepositorySnapshotCache
{
    public interface IRepositorySnapshotCache<T> where T : AggregateRoot
    {
        T GetOrDefault(string key);
        void Set(string key, T value);
    }
}
