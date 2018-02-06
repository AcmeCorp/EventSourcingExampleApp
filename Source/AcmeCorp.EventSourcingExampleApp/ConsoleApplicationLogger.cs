namespace AcmeCorp.EventSourcingExampleApp
{
    public class ConsoleApplicationLogger : IApplicationLogger
    {
        public void HandlerProcessingMessage(object handler, object message)
        {
            this.Info(string.Empty);
            string handlerFullname = handler.GetType().FullName;
            handlerFullname = handlerFullname.Substring(33);
            handlerFullname = handlerFullname.Substring(0, handlerFullname.IndexOf('.'));
            this.Info($"'{handlerFullname}' is handling message '{message.GetType().Name}'.");
        }

        public void PublishMessage(object message)
        {
            this.Info(string.Empty);
            this.Info($"Publishing message '{message.GetType().Name}'.");
        }

        public void SendMessage(object message)
        {
            this.Info(string.Empty);
            this.Info($"Sending message '{message.GetType().Name}'.");
        }

        public void Info(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}