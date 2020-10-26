using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace AwayFromKeyboard.Test.Framework
{
    public class AggregateConfiguration<T> : IAggregateConfiguration<T> where T : class
    {
        private readonly List<IAggregateEventHandler<T>> _eventHandlers;

        public AggregateConfiguration()
        {
            _eventHandlers = new List<IAggregateEventHandler<T>>();
        }

        public void RegisterHandler<TE>() where TE : class, IAggregateEvent<T>
        {
            _eventHandlers.Add(new AggregateEventHandler<T,TE>(expression => {}));
        }

        public void RegisterHandler<TE>(Action<IMappingExpression<TE, T>> configureMapping) where TE : class, IAggregateEvent<T>
        {
            _eventHandlers.Add(new AggregateEventHandler<T, TE>(configureMapping));
        }

        public void RegisterHandler<TE>(Action<T, TE> handler) where TE: class, IAggregateEvent<T>
        {
            _eventHandlers.Add(new AggregateEventHandler<T,TE>(handler));
        }

        public IAggregateEventHandler<T> GetEventHandler(IAggregateEvent<T> aggregateEvent)
        {
            return _eventHandlers.FirstOrDefault(h => h.CanHandle(aggregateEvent));
        }
    }
}
