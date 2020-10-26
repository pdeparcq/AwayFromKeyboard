using System;
using AutoMapper;
using AwayFromKeyboard.Test.Domain;
using AwayFromKeyboard.Test.Framework;
using NUnit.Framework;

namespace AwayFromKeyboard.Test
{
    [TestFixture]
    public class AggregateConfigurationTests
    {

        [Test]
        public void CanRegisterDefaultHandler()
        {
            // Arrange
            var configuration = new AggregateConfiguration<Touchpoint>();
            var touchpoint = new Touchpoint();
            var aggregateEvent = new TouchpointCreated
            {
                Id = Guid.NewGuid(),
                Name = "TestTouchpoint"
            };

            // Act
            configuration.RegisterHandler<TouchpointCreated>();

            // Assert
            var handler = configuration.GetEventHandler(aggregateEvent);
            Assert.NotNull(handler);
            
            handler.Handle(touchpoint, aggregateEvent);
            Assert.AreEqual(aggregateEvent.Id, touchpoint.Id);
            Assert.AreEqual(aggregateEvent.Name, touchpoint.Name);
        }


        [Test]
        public void CanRegisterMappingHandler()
        {
            // Arrange
            var configuration = new AggregateConfiguration<Touchpoint>();
            var touchpoint = new Touchpoint();
            var aggregateEvent = new TouchpointCreated
            {
                Id = Guid.NewGuid(),
                Name = "TestTouchpoint"
            };

            // Act
            configuration.RegisterHandler<TouchpointCreated>(expression =>
                {
                    expression.ForMember(x => x.Name, opt => opt.MapFrom(x => "CustomName"));
                });

            // Assert
            var handler = configuration.GetEventHandler(aggregateEvent);
            Assert.NotNull(handler);

            handler.Handle(touchpoint, aggregateEvent);
            Assert.AreEqual(aggregateEvent.Id, touchpoint.Id);
            Assert.AreEqual("CustomName", touchpoint.Name);
        }

        [Test]
        public void CanRegisterCustomHandler()
        {
            // Arrange
            var configuration = new AggregateConfiguration<Touchpoint>();
            var touchpoint = new Touchpoint();
            var aggregateEvent = new TouchpointCreated
            {
                Id = Guid.NewGuid(),
                Name = "TestTouchpoint"
            };

            // Act
            configuration.RegisterHandler((Touchpoint t, TouchpointCreated te) =>
            {
                t.Id = te.Id;
                t.Name = te.Name;
            });

            // Assert
            var handler = configuration.GetEventHandler(aggregateEvent);
            Assert.NotNull(handler);

            handler.Handle(touchpoint, aggregateEvent);
            Assert.AreEqual(aggregateEvent.Id, touchpoint.Id);
            Assert.AreEqual(aggregateEvent.Name, touchpoint.Name);
        }
    }
}
