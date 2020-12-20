using System;
using System.ComponentModel.DataAnnotations;

namespace AwayFromKeyboard.Api.InputModels
{
    public class CreateModule
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public Guid? ParentModuleId { get; set; }
    }
}
