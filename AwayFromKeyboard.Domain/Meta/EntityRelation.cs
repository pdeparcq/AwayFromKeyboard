using System;

namespace AwayFromKeyboard.Domain.Meta
{
    public enum Multiplicity
    {
        One,
        Many
    }

    public class EntityRelation
    {
        public Guid FromEntityId { get; set; }
        public virtual Entity FromEntity { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ToEntityId { get; set; }
        public virtual Entity ToEntity { get; set; }
        public Multiplicity Multiplicity { get; set; }
    }
}
