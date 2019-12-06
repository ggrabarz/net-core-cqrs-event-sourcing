namespace NetCoreCqrs.Api.Core.Events
{
    public class Event : IEvent
    {
        public int Version { get; set; }
    }
}

