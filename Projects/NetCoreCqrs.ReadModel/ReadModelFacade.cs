using NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels;
using System;
using System.Collections.Generic;

namespace NetCoreCqrs.ReadModel
{
    public class ReadModelFacade : IReadModelFacade
    {
        public IEnumerable<InventoryItemListDto> GetInventoryItems()
        {
            return FakeReadDatabase.InventoryItemsMaterialisedView;
        }

        public InventoryItemDetailsDto GetInventoryItemDetailsOrDefault(Guid id)
        {
            if (!FakeReadDatabase.InventoryItemsDetailsMaterialisedView.ContainsKey(id))
            {
                return null;
            }
            return FakeReadDatabase.InventoryItemsDetailsMaterialisedView[id];
        }
    }
}
