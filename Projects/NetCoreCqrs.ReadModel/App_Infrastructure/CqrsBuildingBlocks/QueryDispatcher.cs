using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            var handlers = GetServices<IQueryHandler<TQuery, TResult>>(_serviceProvider);
            if (handlers != null && handlers.Any())
            {
                if (handlers.Count() != 1) throw new InvalidOperationException($"cannot send {typeof(TQuery).Name} to more than one handler");
                return await handlers.First().HandleAsync(query);
            }
            else
            {
                throw new InvalidOperationException($"no handler registered for {typeof(TQuery).Name}");
            }
        }

        private static IEnumerable<T> GetServices<T>(IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            return (IEnumerable<T>)provider.GetService(typeof(IEnumerable<T>));
        }
    }
}
