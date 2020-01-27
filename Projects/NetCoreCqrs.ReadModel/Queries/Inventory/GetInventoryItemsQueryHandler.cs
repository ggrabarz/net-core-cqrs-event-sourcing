using NetCoreCqrs.ReadModel.App_Infrastructure.CqrsBuildingBlocks;
using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCqrs.ReadModel.Queries.Inventory
{
    public class GetInventoryItemsQueryHandler : IQueryHandler<GetInventoryItemsQuery, IEnumerable<InventoryItemListDto>>
    {
        private readonly IReadModelFacade _readModelFacade;

        public GetInventoryItemsQueryHandler(IReadModelFacade readModelFacade)
        {
            _readModelFacade = readModelFacade;
        }

        public async Task<IEnumerable<InventoryItemListDto>> HandleAsync(GetInventoryItemsQuery query)
        {
            return await Task.FromResult(_readModelFacade.GetInventoryItems().ToList());
        }
    }
}
