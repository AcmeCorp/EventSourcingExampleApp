namespace AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;

    public class OrderPlacedV1 : IOrderPlacedV1
    {
        public OrderPlacedV1(int orderId, int customerId)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.OrderDetails = new OrderDetails();
        }

        public int CustomerId { get; }

        public int OrderId { get; }

        public OrderDetails OrderDetails { get; }
    }
}