﻿using System;

namespace NetCoreCqrs.Api.Core.ReadModel
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
