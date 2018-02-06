namespace AcmeCorp.EventSourcingExampleApp.Orders.Domain
{
    public class OrderAggregateFactory : IOrderAggregateFactory
    {
        public IOrderAggregate Create(int orderId)
        {
            return new OrderAggregate($"Order-{orderId}");
        }
    }
}