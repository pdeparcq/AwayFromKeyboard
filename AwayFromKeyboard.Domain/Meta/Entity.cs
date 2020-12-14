using System;
using System.Collections.Generic;

namespace AwayFromKeyboard.Domain.Meta
{
    public class Entity : BaseType
    {
        public virtual ICollection<EntityRelation> Relations { get; set; }
    }
}
