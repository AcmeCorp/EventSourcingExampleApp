namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.MessageHandlers
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;

    public class DeliveryOptionsSubmittedV1Handler : IHandleMessage<IDeliveryOptionsSubmittedV1>
    {
        private readonly IDeliveryOptionsDataStore deliveryOptionsDataStore;
        private readonly IApplicationLogger applicationLogger;

        public DeliveryOptionsSubmittedV1Handler(IDeliveryOptionsDataStore deliveryOptionsDataStore, IApplicationLogger applicationLogger)
        {
            this.deliveryOptionsDataStore = deliveryOptionsDataStore;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(IDeliveryOptionsSubmittedV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            this.deliveryOptionsDataStore.Save(message.OrderId, message.DeliveryOptions);
        }
    }
}
