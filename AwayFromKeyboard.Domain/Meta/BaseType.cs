using System;
using System.Collections.Generic;

namespace AwayFromKeyboard.Domain.Meta
{
    public class BaseType
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
