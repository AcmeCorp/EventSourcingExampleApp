namespace AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events
{
    public class OrderFulfilledV1 : IOrderFulfilledV1
    {
        public OrderFulfilledV1(int orderId, int customerId)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }

        public int OrderId { get; }

        public int CustomerId { get; }
    }
}
