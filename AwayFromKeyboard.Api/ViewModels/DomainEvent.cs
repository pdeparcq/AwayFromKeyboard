using System.Collections.Generic;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class DomainEvent : BaseType
    {
    }

    public class DomainEventDetails : BaseType
    {
        public List<Property> Properties { get; set; }
    }
}
