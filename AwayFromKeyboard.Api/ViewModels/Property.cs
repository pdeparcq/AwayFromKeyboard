using System;
using AwayFromKeyboard.Domain.Meta;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class Property
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCollection { get; set; }
        public bool IsIdentity { get; set; }
        public SystemType? SystemType { get; set; }
        public Guid? ValueTypeId { get; set; }
    }
}
