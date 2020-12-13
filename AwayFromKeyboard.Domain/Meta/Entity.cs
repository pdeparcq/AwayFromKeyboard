using System;
using System.Collections.Generic;

namespace AwayFromKeyboard.Domain.Meta
{
    public class Entity : BaseType
    {
        public Guid IdentityId { get; set; }
        public virtual ValueObject Identity { get; set; }
        public virtual ICollection<EntityRelation> Relations { get; set; }
    }
}
