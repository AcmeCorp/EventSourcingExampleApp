namespace AcmeCorp.EventSourcingExampleApp
{
    public interface IApplicationLogger
    {
        void HandlerProcessingMessage(object handler, object message);

        void PublishMessage(object message);

        void SendMessage(object message);

        void Info(string message);
    }
}
