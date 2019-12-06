using System;

namespace NetCoreCqrs.Api.Core.Domain
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(T aggregate, int expectedVersion);
        T GetById(Guid id);
    }
}
