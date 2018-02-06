namespace AcmeCorp.EventSourcingExampleApp
{
    public interface IBus
    {
        void Publish(object message);

        void Send(object message);
    }
}