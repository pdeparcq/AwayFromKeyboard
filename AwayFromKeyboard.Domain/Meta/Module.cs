using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AwayFromKeyboard.Domain.Meta
{
    public class Module
    {
        public Guid? ParentModuleId { get; set; }
        [JsonIgnore]
        public Module ParentModule { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Module> SubModules { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
        public virtual ICollection<ValueObject> ValueObjects { get; set; }
    }
}
