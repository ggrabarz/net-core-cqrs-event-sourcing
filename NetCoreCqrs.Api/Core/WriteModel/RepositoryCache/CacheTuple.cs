using System;

namespace NetCoreCqrs.Api.Core.WriteModel.RepositoryCache
{
    internal struct CacheTuple<T>
    {
        public T Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}
