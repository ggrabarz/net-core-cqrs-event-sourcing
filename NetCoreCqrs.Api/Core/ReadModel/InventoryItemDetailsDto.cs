using System;

namespace NetCoreCqrs.Api.Core.ReadModel
{
    public class InventoryItemDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CurrentCount { get; set; }
        public int Version { get; set; }

        public InventoryItemDetailsDto(Guid id, string name, int currentCount, int version)
        {
            Id = id;
            Name = name;
            CurrentCount = currentCount;
            Version = version;
        }
    }
}
