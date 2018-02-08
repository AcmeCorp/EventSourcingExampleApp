namespace AcmeCorp.EventSourcingExampleApp.Orders.MessageHandlers
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Orders.Domain;

    public class PlaceOrderV1Handler : IHandleMessage<PlaceOrderV1>
    {
        private readonly IDomainRepository domainRepository;
        private readonly IOrderAggregateFactory orderAggregateFactory;
        private readonly IApplicationLogger applicationLogger;

        public PlaceOrderV1Handler(IDomainRepository domainRepository, IOrderAggregateFactory orderAggregateFactory, IApplicationLogger applicationLogger)
        {
            this.domainRepository = domainRepository;
            this.orderAggregateFactory = orderAggregateFactory;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(PlaceOrderV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            IOrderAggregate orderAggregate = this.orderAggregateFactory.Create(message.OrderId);
            this.domainRepository.LoadIfExistsAsync(orderAggregate);
            orderAggregate.PlaceOrder(message.OrderId, message.CustomerId, message.OrderDetails);
            this.domainRepository.SaveAsync(orderAggregate);
        }
    }
}
