namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain
{
    public interface IDeliveryAggregateFactory
    {
        IDeliveryAggregate Create(int orderId);
    }
}
