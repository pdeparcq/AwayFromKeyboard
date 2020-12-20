using AwayFromKeyboard.Api.Domain.Meta;

namespace AwayFromKeyboard.Api.ViewModels
{
    public class EntityRelation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Entity ToEntity { get; set; }
        public Multiplicity Multiplicity { get; set; }
    }
}
