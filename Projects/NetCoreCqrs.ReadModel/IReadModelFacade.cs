using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System;
using System.Collections.Generic;

namespace NetCoreCqrs.ReadModel
{
    public interface IReadModelFacade
    {
        IEnumerable<InventoryItemListDto> GetInventoryItems();
        InventoryItemDetailsDto GetInventoryItemDetailsOrDefault(Guid id);
    }
}
