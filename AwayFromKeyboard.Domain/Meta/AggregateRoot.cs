using System.Collections.Generic;

namespace AwayFromKeyboard.Domain.Meta
{
    public class AggregateRoot : Entity
    {
        public virtual ICollection<DomainEvent> DomainEvents { get; set; }
    }
}
