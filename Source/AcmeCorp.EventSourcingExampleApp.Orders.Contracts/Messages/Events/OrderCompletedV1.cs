namespace AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Events
{
    public class OrderCompletedV1 : IOrderCompletedV1
    {
        public OrderCompletedV1(int customerId, int orderId)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
        }

        public int CustomerId { get; }

        public int OrderId { get; }
    }
}
