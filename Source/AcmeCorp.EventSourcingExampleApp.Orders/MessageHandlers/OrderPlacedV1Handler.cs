namespace AcmeCorp.EventSourcingExampleApp.Orders.MessageHandlers
{
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Events;

    public class OrderPlacedV1Handler : IHandleMessage<IOrderPlacedV1>
    {
        private readonly IOrdersDataStore ordersDataStore;
        private readonly IApplicationLogger applicationLogger;

        public OrderPlacedV1Handler(IOrdersDataStore ordersDataStore, IApplicationLogger applicationLogger)
        {
            this.ordersDataStore = ordersDataStore;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(IOrderPlacedV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            this.ordersDataStore.Save(message.OrderId, message.OrderDetails);
        }
    }
}
