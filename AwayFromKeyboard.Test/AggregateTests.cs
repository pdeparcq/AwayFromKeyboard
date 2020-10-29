using System;
using System.Collections.Generic;
using AwayFromKeyboard.Test.Domain;
using AwayFromKeyboard.Test.Framework;
using AwayFromKeyboard.Test.Framework.Exceptions;
using Moq;
using NUnit.Framework;

namespace AwayFromKeyboard.Test
{

    [TestFixture]
    public class AggregateTests
    {
        [Test]
        public void CanCreateAggregate()
        {
            // Arrange
            var configuration = new Mock<IAggregateConfiguration<Touchpoint>>();

            // Act
            var aggregate = new Aggregate<Touchpoint>(configuration.Object);

            // Assert
            Assert.IsNotNull(aggregate.Instance);
        }

        [Test]
        public void CanHandleAggregateEvents()
        {
            // Arrange
            var @event = new TouchpointCreated
            {
                Id = Guid.NewGuid(),
                Name = "TestTouchpointName"
            };

            var eventHandler = new Mock<IAggregateEventHandler<Touchpoint>>();
            eventHandler.Setup(e => e.Handle(It.IsAny<Touchpoint>(), It.IsAny<TouchpointCreated>()))
                .Callback((Touchpoint t, IAggregateEvent<Touchpoint> e) =>
                {
                    var te = e as TouchpointCreated;
                    t.Id = te.Id;
                    t.Name = te.Name;
                });
            
            var configuration = new Mock<IAggregateConfiguration<Touchpoint>>();
            configuration.Setup(c => c.GetEventHandler(It.IsAny<TouchpointCreated>()))
                .Returns(eventHandler.Object);

            // Act
            var aggregate = new Aggregate<Touchpoint>(configuration.Object);
            aggregate.Handle(@event);

            // Assert
            Assert.IsNotNull(aggregate.Instance);
            Assert.AreEqual(@event.Id, aggregate.Instance.Id);
            Assert.AreEqual(@event.Name, aggregate.Instance.Name);
        }

        [Test]
        public void ThrowsExceptionWhenHandlerNotFound()
        {
            // Arrange
            var @event = new TouchpointCreated
            {
                Id = Guid.NewGuid(),
                Name = "TestTouchpointName"
            };

            var configuration = new Mock<IAggregateConfiguration<Touchpoint>>();
            configuration.Setup(c => c.GetEventHandler(It.IsAny<TouchpointCreated>()))
                .Returns(() => null);

            // Act
            var aggregate = new Aggregate<Touchpoint>(configuration.Object);
            
            // Assert
            Assert.Throws<HandlerNotFoundException>(() => aggregate.Handle(@event));
        }

        [Test]
        public void CanLoadAggregateFromEvents()
        {
            // Arrange
            var touchpointId = Guid.NewGuid();

            var createdEventHandler = new Mock<IAggregateEventHandler<Touchpoint>>();
            createdEventHandler.Setup(e => e.Handle(It.IsAny<Touchpoint>(), It.IsAny<TouchpointCreated>()))
                .Callback((Touchpoint t, IAggregateEvent<Touchpoint> e) =>
                {
                    var te = e as TouchpointCreated;
                    t.Id = te.Id;
                    t.Name = te.Name;
                });
            var changedEventHandler = new Mock<IAggregateEventHandler<Touchpoint>>();
            changedEventHandler.Setup(e => e.Handle(It.IsAny<Touchpoint>(), It.IsAny<TouchpointNameChanged>()))
                .Callback((Touchpoint t, IAggregateEvent<Touchpoint> e) =>
                {
                    var te = e as TouchpointNameChanged;

                    t.Name = te.Name;
                });

            var configuration = new Mock<IAggregateConfiguration<Touchpoint>>();
            configuration.Setup(c => c.GetEventHandler(It.IsAny<TouchpointCreated>()))
                .Returns(createdEventHandler.Object);
            configuration.Setup(c => c.GetEventHandler(It.IsAny<TouchpointNameChanged>()))
                .Returns(changedEventHandler.Object);

            // Act
            var aggregate = new Aggregate<Touchpoint>(configuration.Object);
            aggregate.Load(new List<IAggregateEvent<Touchpoint>>()
            {
                new TouchpointCreated
                {
                    Id = touchpointId,
                    Name = "TestTouchpoint"
                },
                new TouchpointNameChanged()
                {
                    Name = "TestTouchpointRenamed"
                }
            });

            // Assert
            Assert.IsNotNull(aggregate.Instance);
            Assert.AreEqual(touchpointId, aggregate.Instance.Id);
            Assert.AreEqual("TestTouchpointRenamed", aggregate.Instance.Name);
        }
    }
}
