namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.MessageHandlers
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain;

    public class OrderAcceptedV1Handler : IHandleMessage<IOrderAcceptedV1>
    {
        private readonly IDomainRepository domainRepository;
        private readonly IDeliveryAggregateFactory deliveryAggregateFactory;
        private readonly IApplicationLogger applicationLogger;

        public OrderAcceptedV1Handler(IDomainRepository domainRepository, IDeliveryAggregateFactory deliveryAggregateFactory, IApplicationLogger applicationLogger)
        {
            this.domainRepository = domainRepository;
            this.deliveryAggregateFactory = deliveryAggregateFactory;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(IOrderAcceptedV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            IDeliveryAggregate deliveryAggregate = this.deliveryAggregateFactory.Create(message.OrderId);
            this.domainRepository.LoadAsync(deliveryAggregate);
            deliveryAggregate.AcceptOrder(message.OrderId);
            this.domainRepository.SaveAsync(deliveryAggregate);
        }
    }
}