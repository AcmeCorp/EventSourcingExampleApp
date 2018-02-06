namespace AcmeCorp.EventSourcingExampleApp.Payments.MessageHandlers
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;

    public class SubmitPaymentDetailsV1Handler : IHandleMessage<SubmitPaymentDetailsV1>
    {
        private readonly IPaymentsDataStore paymentsDataStore;
        private readonly IApplicationLogger applicationLogger;

        public SubmitPaymentDetailsV1Handler(IPaymentsDataStore paymentsDataStore, IApplicationLogger applicationLogger)
        {
            this.paymentsDataStore = paymentsDataStore;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(SubmitPaymentDetailsV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            this.paymentsDataStore.Save(message.OrderId, message.PaymentDetails);
        }
    }
}
