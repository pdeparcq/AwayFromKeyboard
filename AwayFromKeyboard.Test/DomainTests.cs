using AwayFromKeyboard.Test.Domain;
using AwayFromKeyboard.Test.Framework.Exceptions;
using NUnit.Framework;

namespace AwayFromKeyboard.Test
{
    [TestFixture]
    public class DomainTests
    {

        [Test]
        public void ThrowsExceptionWhenAggregateNotFound()
        {
            // Arrange
            var domain = new Framework.Domain();

            // Act + Assert
            Assert.Throws<AggregateNotFoundException>(() => domain.CreateAggregate<Touchpoint>());
        }
    }
}
