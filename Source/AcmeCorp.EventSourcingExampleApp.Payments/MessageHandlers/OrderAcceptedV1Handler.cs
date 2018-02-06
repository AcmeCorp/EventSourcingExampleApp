namespace AcmeCorp.EventSourcingExampleApp.Payments.MessageHandlers
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;

    public class OrderAcceptedV1Handler : IHandleMessage<IOrderAcceptedV1>
    {
        private readonly IBus bus;
        private readonly IPaymentsDataStore paymentsDataStore;
        private readonly IApplicationLogger applicationLogger;

        public OrderAcceptedV1Handler(IBus bus, IPaymentsDataStore paymentsDataStore, IApplicationLogger applicationLogger)
        {
            this.bus = bus;
            this.paymentsDataStore = paymentsDataStore;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(IOrderAcceptedV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            if (this.paymentsDataStore.CheckExists(message.OrderId))
            {
                ProcessPaymentV1 processPaymentV1 = new ProcessPaymentV1(message.OrderId, message.CustomerId);
                this.applicationLogger.SendMessage(processPaymentV1);
                this.bus.Send(processPaymentV1);
            }
            else
            {
                // Payment data not found, something went wrong.
                // Perform some compensating actions (e.g. "Could Not Process Payment" event or something) so
                // that the other services can react and act accordingly (cancel the order or advise customer).
                // Put an exception for now (though note that for real world applications you should never throw a "logical" exception).
                throw new EventSourcingExampleAppException("The payment details for the order could not be found. This should really be an event like 'Could Not Process Payment'.");
            }
        }
    }
}