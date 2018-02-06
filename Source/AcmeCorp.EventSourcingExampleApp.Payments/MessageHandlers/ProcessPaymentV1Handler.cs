namespace AcmeCorp.EventSourcingExampleApp.Payments.MessageHandlers
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;

    public class ProcessPaymentV1Handler : IHandleMessage<ProcessPaymentV1>
    {
        private readonly IBus bus;
        private readonly IPaymentsDataStore paymentsDataStore;
        private readonly ICreditCardPaymentProvider creditCardPaymentProvider;
        private readonly IApplicationLogger applicationLogger;

        public ProcessPaymentV1Handler(IBus bus, IPaymentsDataStore paymentsDataStore, ICreditCardPaymentProvider creditCardPaymentProvider, IApplicationLogger applicationLogger)
        {
            this.bus = bus;
            this.paymentsDataStore = paymentsDataStore;
            this.creditCardPaymentProvider = creditCardPaymentProvider;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(ProcessPaymentV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            PaymentDetails paymentDetails = this.paymentsDataStore.Load(message.OrderId);
            this.creditCardPaymentProvider.ProcessPayment(paymentDetails.PaymentId, paymentDetails.CardNumber, paymentDetails.Expiry, paymentDetails.Ccv, paymentDetails.Amount);
            IPaymentCompletedV1 paymentCompletedV1 = new PaymentCompletedV1(message.CustomerId, message.OrderId, paymentDetails.PaymentId);
            this.applicationLogger.PublishMessage(paymentCompletedV1);
            this.bus.Publish(paymentCompletedV1);
        }
    }
}
