using System.ComponentModel.DataAnnotations;

namespace AwayFromKeyboard.Api.InputModels
{
    public class AddDomainEvent
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
