namespace AcmeCorp.EventSourcingExampleApp.Orders.MessageHandlers
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Orders.Domain;

    public class OrderFulfilledV1Handler : IHandleMessage<IOrderFulfilledV1>
    {
        private readonly IDomainRepository domainRepository;
        private readonly IOrderAggregateFactory orderAggregateFactory;
        private readonly IApplicationLogger applicationLogger;

        public OrderFulfilledV1Handler(IDomainRepository domainRepository, IOrderAggregateFactory orderAggregateFactory, IApplicationLogger applicationLogger)
        {
            this.domainRepository = domainRepository;
            this.orderAggregateFactory = orderAggregateFactory;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(IOrderFulfilledV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            IOrderAggregate orderAggregate = this.orderAggregateFactory.Create(message.OrderId);
            this.domainRepository.LoadAsync(orderAggregate);
            orderAggregate.OrderSentToCustomer(message.OrderId, message.CustomerId);
            this.domainRepository.SaveAsync(orderAggregate);
        }
    }
}