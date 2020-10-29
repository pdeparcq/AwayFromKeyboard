using System;
using AwayFromKeyboard.Test.Framework;

namespace AwayFromKeyboard.Test.Domain
{
    public class Touchpoint
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class TouchpointCreated : IAggregateEvent<Touchpoint>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class TouchpointNameChanged : IAggregateEvent<Touchpoint>
    {
        public string Name { get; set; }
    }
}
