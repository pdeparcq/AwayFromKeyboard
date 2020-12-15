using System;

namespace AwayFromKeyboard.Api.InputModels
{
    public class CreateEntity
    {
        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
