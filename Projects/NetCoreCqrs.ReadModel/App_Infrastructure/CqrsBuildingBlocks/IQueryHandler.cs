using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
