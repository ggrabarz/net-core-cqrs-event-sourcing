using System;
using System.Collections.Generic;

namespace NetCoreCqrs.Api.Core.ReadModel
{
    public interface IReadModelFacade
    {
        IEnumerable<InventoryItemListDto> GetInventoryItems();
        InventoryItemDetailsDto GetInventoryItemDetailsOrDefault(Guid id);
    }
}
