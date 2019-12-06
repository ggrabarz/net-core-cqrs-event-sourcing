using NetCoreCqrs.Api.Core.Events;

namespace NetCoreCqrs.Api.Core.FakeBus
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event) where T : IEvent;
    }
}
