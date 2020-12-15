using System;
using System.Collections.Generic;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class EntityDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Property> Properties { get; set; }
    }
}
