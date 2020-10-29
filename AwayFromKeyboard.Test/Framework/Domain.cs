using System;
using System.Collections.Generic;
using AwayFromKeyboard.Test.Framework.Exceptions;

namespace AwayFromKeyboard.Test.Framework
{
    public class Domain
    {
        private readonly Dictionary<Type, object> _aggregates;

        public Domain()
        {
            _aggregates = new Dictionary<Type, object>();
        }

        private IAggregateConfiguration<T> GetAggregateConfiguration<T>() where T:class
        {
            if(!_aggregates.ContainsKey(typeof(T)))
                throw new AggregateNotFoundException(typeof(T).Name);
            return _aggregates[typeof(T)] as IAggregateConfiguration<T>;
        }
    
        public Aggregate<T> CreateAggregate<T>() where T : class
        {
            return new Aggregate<T>(GetAggregateConfiguration<T>());
        }
    }
}
