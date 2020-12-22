using System.ComponentModel.DataAnnotations;
using AwayFromKeyboard.Api.Domain.CodeGen;

namespace AwayFromKeyboard.Api.InputModels
{
    public class CreateTemplate
    { 
        public MetaType MetaType { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }
    }
}
