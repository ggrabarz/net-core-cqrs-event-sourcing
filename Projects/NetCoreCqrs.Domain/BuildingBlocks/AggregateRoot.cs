using NetCoreCqrs.Domain.App_Infrastructure.PrivateReflection;
using System;
using System.Collections.Generic;

namespace NetCoreCqrs.Domain.BuildingBlocks
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public abstract Guid Id { get; }
        public int Version { get; internal set; } = -1; // create event application will increment this value to zero

        public IReadOnlyList<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        // aka rehydration process
        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            Version++;
            if (isNew) _changes.Add(@event);
        }
    }
}
