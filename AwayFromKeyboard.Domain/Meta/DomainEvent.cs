using System;

namespace AwayFromKeyboard.Domain.Meta
{
    public class DomainEvent : BaseType
    {
        public Guid AggregateRootId { get; set; }
        public virtual AggregateRoot AggregateRoot { get; set; }
    }
}
