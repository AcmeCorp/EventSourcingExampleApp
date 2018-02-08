namespace AcmeCorp.EventSourcingExampleApp.Orders.MessageHandlers
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Orders.Domain;

    public class ConfirmOrderV1Handler : IHandleMessage<ConfirmOrderV1>
    {
        private readonly IDomainRepository domainRepository;
        private readonly IOrderAggregateFactory orderAggregateFactory;
        private readonly IApplicationLogger applicationLogger;

        public ConfirmOrderV1Handler(IDomainRepository domainRepository, IOrderAggregateFactory orderAggregateFactory, IApplicationLogger applicationLogger)
        {
            this.domainRepository = domainRepository;
            this.orderAggregateFactory = orderAggregateFactory;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(ConfirmOrderV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            IOrderAggregate orderAggregate = this.orderAggregateFactory.Create(message.OrderId);
            this.domainRepository.LoadAsync(orderAggregate);
            orderAggregate.ConfirmOrder(message.OrderId, message.CustomerId);
            this.domainRepository.SaveAsync(orderAggregate);
        }
    }
}