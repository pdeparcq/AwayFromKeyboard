using System;
using AwayFromKeyboard.Api.Domain.CodeGen;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class Template
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MetaType MetaType { get; set; }
        public string Value { get; set; }
    }
}
