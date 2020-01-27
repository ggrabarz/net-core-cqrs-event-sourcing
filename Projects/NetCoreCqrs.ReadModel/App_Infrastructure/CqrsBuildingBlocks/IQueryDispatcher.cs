using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
