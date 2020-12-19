using System;

namespace AwayFromKeyboard.Api.Domain.Meta
{
    public class DomainEvent : BaseType
    {
        public Guid AggregateRootId { get; set; }
        public virtual Entity AggregateRoot { get; set; }
    }
}
