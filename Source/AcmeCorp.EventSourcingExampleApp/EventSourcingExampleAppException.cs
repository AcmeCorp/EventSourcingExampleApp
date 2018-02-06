namespace AcmeCorp.EventSourcingExampleApp
{
    using System;
    using System.Runtime.Serialization;

    public class EventSourcingExampleAppException : Exception
    {
        public EventSourcingExampleAppException()
        {
        }

        public EventSourcingExampleAppException(string message)
            : base(message)
        {
        }

        public EventSourcingExampleAppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EventSourcingExampleAppException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
