namespace AcmeCorp.EventSourcingExampleApp.Orders.Domain
{
    public interface IOrderAggregateFactory
    {
        IOrderAggregate Create(int orderId);
    }
}
