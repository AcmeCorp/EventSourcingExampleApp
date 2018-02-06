namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.MessageHandlers
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain;

    public class PaymentCompletedV1Handler : IHandleMessage<IPaymentCompletedV1>
    {
        private readonly IDomainRepository domainRepository;
        private readonly IDeliveryAggregateFactory deliveryAggregateFactory;
        private readonly IApplicationLogger applicationLogger;

        public PaymentCompletedV1Handler(IDomainRepository domainRepository, IDeliveryAggregateFactory deliveryAggregateFactory, IApplicationLogger applicationLogger)
        {
            this.domainRepository = domainRepository;
            this.deliveryAggregateFactory = deliveryAggregateFactory;
            this.applicationLogger = applicationLogger;
        }

        public void Handle(IPaymentCompletedV1 message)
        {
            this.applicationLogger.HandlerProcessingMessage(this, message);
            IDeliveryAggregate deliveryAggregate = this.deliveryAggregateFactory.Create(message.OrderId);
            this.domainRepository.LoadAsync(deliveryAggregate);
            deliveryAggregate.CompletePayment(message.OrderId);
            this.domainRepository.SaveAsync(deliveryAggregate);
        }
    }
}
