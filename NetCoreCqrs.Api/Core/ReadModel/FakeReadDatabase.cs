using System;
using System.Collections.Generic;

namespace NetCoreCqrs.Api.Core.ReadModel
{
    public class FakeReadDatabase
    {
        public static Dictionary<Guid, InventoryItemDetailsDto> InventoryItemsDetailsMaterialisedView = new Dictionary<Guid, InventoryItemDetailsDto>();
        public static List<InventoryItemListDto> InventoryItemsMaterialisedView = new List<InventoryItemListDto>();
    }
}
