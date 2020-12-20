using System;
using System.ComponentModel.DataAnnotations;

namespace AwayFromKeyboard.Api.InputModels
{
    public class CreateType
    {
        [Required]
        public Guid ModuleId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}
