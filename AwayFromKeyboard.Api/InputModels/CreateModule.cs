using System;

namespace AwayFromKeyboard.Api.InputModels
{
    public class CreateModule
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentModuleId { get; set; }
    }
}
