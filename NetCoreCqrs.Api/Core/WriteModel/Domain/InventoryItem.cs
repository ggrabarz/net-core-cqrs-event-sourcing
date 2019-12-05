using NetCoreCqrs.Api.Core.Events;
using System;

namespace NetCoreCqrs.Api.Core.Domain
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter")]


    public class InventoryItem : AggregateRoot
    {
        private bool _activated;
        private Guid _id;
        private string _name;
        private int _count;

        public InventoryItem()
        {
        }

        public InventoryItem(Guid id, string name)
        {
            ApplyChange(new InventoryItemCreatedEvent(id, name));
        }

        private void Apply(InventoryItemCreatedEvent e)
        {
            _id = e.Id;
            _activated = true;
            _name = e.Name;
            _count = 0;
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException("newName");
            ApplyChange(new InventoryItemRenamedEvent(_id, newName));
        }

        private void Apply(InventoryItemRenamedEvent e)
        {
            _name = e.NewName;
        }

        public void Remove(int count)
        {
            if (count <= 0) throw new InvalidOperationException("cant remove negative count from inventory");
            ApplyChange(new ItemsRemovedFromInventoryEvent(_id, count));
        }

        private void Apply(ItemsRemovedFromInventoryEvent e)
        {
            _count -= e.Count;
        }


        public void CheckIn(int count)
        {
            if (count <= 0) throw new InvalidOperationException("must have a count greater than 0 to add to inventory");
            ApplyChange(new ItemsCheckedInToInventoryEvent(_id, count));
        }

        private void Apply(ItemsCheckedInToInventoryEvent e)
        {
            _count += e.Count;
        }

        public void Deactivate()
        {
            if (!_activated) throw new InvalidOperationException("already deactivated");
            ApplyChange(new InventoryItemDeactivatedEvent(_id));
        }

        private void Apply(InventoryItemDeactivatedEvent e)
        {
            _activated = false;
        }

        public override Guid Id
        {
            get { return _id; }
        }        
    }
}
