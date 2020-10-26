using System;
using System.Collections.Generic;
using AwayFromKeyboard.Test.Framework.Exceptions;

namespace AwayFromKeyboard.Test.Framework
{
    public class Aggregate<T> where T:class
    {
        private readonly IAggregateConfiguration<T> _configuration;

        public Aggregate(IAggregateConfiguration<T> configuration)
        {
            _configuration = configuration;
            Instance = Activator.CreateInstance<T>();
        }

        public T Instance { get; }

        public void Load(IEnumerable<IAggregateEvent<T>> events)
        {
            foreach (var @event in events)
            {
                Handle(@event);
            }
        }

        public void Handle(IAggregateEvent<T> aggregateEvent)
        {
            var handler = _configuration.GetEventHandler(aggregateEvent); 
            if(handler == null)
                throw new HandlerNotFoundException($"No handler found for {typeof(T).Name}: {aggregateEvent.GetType().Name}");
            handler.Handle(Instance, aggregateEvent);
        }
    }
}
