using System;

namespace AwayFromKeyboard.Api.InputModels
{
    public class CreateType
    {
        public Guid ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
