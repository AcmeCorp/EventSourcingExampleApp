namespace AcmeCorp.EventSourcingExampleApp
{
    using System;

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
            this.Info("==================");
            this.Info($"Publishing message '{message.GetType().FullName}'.");
        }

        public void SendMessage(object message)
        {
            this.Info(string.Empty);
            this.Info("===============");
            this.Info($"Sending message '{message.GetType().FullName}'.");
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}