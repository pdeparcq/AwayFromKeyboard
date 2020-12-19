﻿using System;
using AwayFromKeyboard.Api.Domain.Meta;

namespace AwayFromKeyboard.Api.InputModels
{
    public class AddProperty
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCollection { get; set; }
        public bool IsIdentity { get; set; }
        public SystemType? SystemType { get; set; }
        public Guid? ValueTypeId { get; set; }
    }
}
