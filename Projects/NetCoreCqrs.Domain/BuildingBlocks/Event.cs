namespace NetCoreCqrs.Domain.BuildingBlocks
{
    public class Event : IDomainEvent
    {
        public int Version { get; set; }
    }
}

