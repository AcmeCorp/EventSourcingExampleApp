namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Dto;

    public interface IDeliveryAggregate : IAggregate
    {
        void SubmitDeliveryOptions(int orderId, DeliveryOptions deliveryOptions);

        void AcceptOrder(int orderId);

        void CompletePayment(int orderId);
    }
}
