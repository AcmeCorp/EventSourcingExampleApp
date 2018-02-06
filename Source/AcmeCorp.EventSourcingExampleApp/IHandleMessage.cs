namespace AcmeCorp.EventSourcingExampleApp
{
    public interface IHandleMessage<T>
    {
        void Handle(T message);
    }
}
