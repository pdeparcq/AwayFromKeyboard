using System;

namespace AwayFromKeyboard.Api.Domain.CodeGen
{

    public enum MetaType
    {
        Module,
        Entity,
        ValueObject,
        DomainEvent
    }

    public class Template
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MetaType MetaType { get; set; }
        public string Value { get; set; }
    }
}
