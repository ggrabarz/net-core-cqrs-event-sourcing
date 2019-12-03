using NetCoreCqrs.Api.Core.Events;

namespace NetCoreCqrs.Api.Core.ReadModel.EventHandlers
{
    public interface IEventHandler<T> where T : IEvent
    {
        void Handle(T message);
    }
}
