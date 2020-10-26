namespace AwayFromKeyboard.Test.Framework
{
    public interface IAggregateEvent<T> where T:class
    {
    }

    public interface IAggregateEventHandler<T> where T : class
    {
        bool CanHandle(IAggregateEvent<T> aggregateEvent);
        void Handle(T aggregate, IAggregateEvent<T> aggregateEvent);
    }
}
