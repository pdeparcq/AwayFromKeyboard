using System.ComponentModel.DataAnnotations;

namespace AwayFromKeyboard.Api.InputModels
{
    public class UpdateTemplate
    {
        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }
    }
}
