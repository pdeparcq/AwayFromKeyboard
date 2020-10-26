using System;
using AutoMapper;

namespace AwayFromKeyboard.Test.Framework
{
    public class AggregateEventHandler<T, TE> : IAggregateEventHandler<T> where T:class where TE: class, IAggregateEvent<T>
    {
        private readonly Action<T, TE> _handle;

        public AggregateEventHandler(Action<T, TE> handle)
        {
            _handle = handle;
        }

        public AggregateEventHandler(Action<IMappingExpression<TE, T>> configureMapping)
        {
            var mapper = new MapperConfiguration(c => configureMapping(c.CreateMap<TE, T>())).CreateMapper();
            _handle = (T aggregate, TE aggregateEvent) =>
            {
                mapper.Map(aggregateEvent, aggregate);
            };
        }

        public bool CanHandle(IAggregateEvent<T> aggregateEvent)
        {
            return aggregateEvent is TE;
        }

        public void Handle(T aggregate, IAggregateEvent<T> aggregateEvent)
        {
            _handle(aggregate, aggregateEvent as TE);
        }
    }
}
