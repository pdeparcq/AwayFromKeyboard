namespace AwayFromKeyboard.Test.Framework
{
    public interface IAggregateConfiguration<T> where T:class
    {
        IAggregateEventHandler<T> GetEventHandler(IAggregateEvent<T> aggregateEvent);
    }
}
