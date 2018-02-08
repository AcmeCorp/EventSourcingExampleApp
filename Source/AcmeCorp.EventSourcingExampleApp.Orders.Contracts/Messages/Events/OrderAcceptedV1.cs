namespace AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Events
{
    public class OrderAcceptedV1 : IOrderAcceptedV1
    {
        public OrderAcceptedV1(int customerId, int orderId)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
        }

        public int CustomerId { get; }

        public int OrderId { get; }
    }
}
