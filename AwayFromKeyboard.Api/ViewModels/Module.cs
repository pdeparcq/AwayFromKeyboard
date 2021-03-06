﻿using System;
using System.Collections.Generic;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class Module
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Entity> Entities { get; set; }
        public List<ValueObject> ValueObjects { get; set; }
    }
}
