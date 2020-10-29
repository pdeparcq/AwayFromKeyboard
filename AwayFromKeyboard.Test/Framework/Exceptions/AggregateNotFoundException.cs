using System;

namespace AwayFromKeyboard.Test.Framework.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message) : base(message) { }
    }
}
