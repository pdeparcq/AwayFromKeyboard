using System;

namespace AwayFromKeyboard.Test.Framework.Exceptions
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(string message) : base(message)
        {
        }
    }
}
