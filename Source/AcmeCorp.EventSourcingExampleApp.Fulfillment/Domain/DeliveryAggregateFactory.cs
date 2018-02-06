namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain
{
    public class DeliveryAggregateFactory : IDeliveryAggregateFactory
    {
        public IDeliveryAggregate Create(int orderId)
        {
            return new DeliveryAggregate($"Delivery-{orderId}");
        }
    }
}