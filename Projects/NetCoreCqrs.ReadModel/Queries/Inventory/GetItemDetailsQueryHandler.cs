using NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.Queries.Inventory
{
    public class GetItemDetailsQueryHandler : IQueryHandler<GetItemDetailsQuery, InventoryItemDetailsDto>
    {
        private readonly IReadModelFacade _readModelFacade;

        public GetItemDetailsQueryHandler(IReadModelFacade readModelFacade)
        {
            _readModelFacade = readModelFacade;
        }

        public async Task<InventoryItemDetailsDto> HandleAsync(GetItemDetailsQuery query)
        {
            return await Task.FromResult(_readModelFacade.GetInventoryItemDetailsOrDefault(query.Id));
        }
    }
}
