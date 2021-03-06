﻿using System;

namespace AwayFromKeyboard.Api.Domain.Meta
{
    public enum SystemType
    {
        String,
        Number,
        Boolean
    }

    public class Property
    {
        public Guid ParentTypeId { get; set; }
        public virtual BaseType ParentType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCollection { get; set; }
        public bool IsIdentity { get; set; }
        public SystemType? SystemType { get; set; }
        public Guid? ValueTypeId { get; set; }
        public ValueObject ValueType { get; set; }
    }
}
