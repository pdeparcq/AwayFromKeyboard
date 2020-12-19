using System.Collections.Generic;
using System.Linq;

namespace AwayFromKeyboard.Domain.Meta
{
    public class Entity : BaseType
    {
        public virtual ICollection<DomainEvent> DomainEvents { get; set; }
        public virtual ICollection<EntityRelation> Relations { get; set; }
        public bool IsAggregateRoot => DomainEvents?.Any() ?? false;
    }
}
