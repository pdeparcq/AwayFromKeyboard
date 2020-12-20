using System;
using System.ComponentModel.DataAnnotations;
using AwayFromKeyboard.Api.Domain.Meta;

namespace AwayFromKeyboard.Api.InputModels
{
    public class AddRelation
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        public string Description { get; set; }

        [Required]
        public Guid ToEntityId { get; set; }

        public Multiplicity Multiplicity { get; set; }
    }
}
