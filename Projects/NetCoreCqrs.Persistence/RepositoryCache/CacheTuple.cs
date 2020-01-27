using System;

namespace NetCoreCqrs.Persistence.RepositoryCache
{
    internal struct CacheTuple<T>
    {
        public T Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}
