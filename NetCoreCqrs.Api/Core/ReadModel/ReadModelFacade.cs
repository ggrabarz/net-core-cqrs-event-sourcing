using System;
using System.Collections.Generic;

namespace NetCoreCqrs.Api.Core.ReadModel
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
