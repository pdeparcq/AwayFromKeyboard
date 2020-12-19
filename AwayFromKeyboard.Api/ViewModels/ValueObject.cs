using System.Collections.Generic;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class ValueObject : BaseType
    {
    }

    public class ValueObjectDetails : BaseType
    {
        public List<Property> Properties { get; set; }
    }
}
