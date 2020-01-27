using System;

namespace NetCoreCqrs.ReadModel.Queries.Inventory.ViewModels
{
    public class InventoryItemListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public InventoryItemListDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
