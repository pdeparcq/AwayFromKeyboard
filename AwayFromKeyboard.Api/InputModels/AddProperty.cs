using System;
using System.ComponentModel.DataAnnotations;
using AwayFromKeyboard.Api.Domain.Meta;

namespace AwayFromKeyboard.Api.InputModels
{
    public class AddProperty
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCollection { get; set; }
        public bool IsIdentity { get; set; }
        public SystemType? SystemType { get; set; }
        public Guid? ValueTypeId { get; set; }
    }
}
