using System;
using System.Collections.Generic;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class Entity : BaseType
    {
        public bool IsAggregateRoot { get; set; }
    }

    public class EntityDetails : Entity
    {
        public List<Property> Properties { get; set; }
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
