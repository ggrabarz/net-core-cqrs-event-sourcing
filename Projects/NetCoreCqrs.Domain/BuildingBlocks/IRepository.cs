using System;

namespace NetCoreCqrs.Domain.BuildingBlocks
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(T aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}
