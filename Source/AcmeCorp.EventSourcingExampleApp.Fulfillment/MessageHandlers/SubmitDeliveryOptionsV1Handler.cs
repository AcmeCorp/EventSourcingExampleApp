namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.MessageHandlers
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain;

    public class SubmitDeliveryOptionsV1Handler : IHandleMessage<SubmitDeliveryOptionsV1>
    {
        private readonly IDomainRepository domainRepository;
        private readonly IDeliveryAggregateFactory deliveryAggregateFactory;
        private readonly IApplicationLogger applicationLogger;

        public SubmitDeliveryOptionsV1Handler(IDomainRepository domainRepository, IDeliveryAggregateFactory deliveryAggregateFactory, IApplicationLogger applicationLogger)
        {
            this.domainRepository = domainRepository;
            this.deliveryAggregateFactory = deliveryAggregateFactory;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(SubmitDeliveryOptionsV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            IDeliveryAggregate deliveryAggregate = this.deliveryAggregateFactory.Create(message.OrderId);
            this.domainRepository.LoadIfExistsAsync(deliveryAggregate);
            deliveryAggregate.SubmitDeliveryOptions(message.OrderId, message.DeliveryOptions);
            this.domainRepository.SaveAsync(deliveryAggregate);
        }
    }
}
