namespace AcmeCorp.EventSourcingExampleApp.Orders.Domain
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Dto;

    public interface IOrderAggregate : IAggregate
    {
        bool OrderComplete { get; }

        void PlaceOrder(int orderId, int customerId, OrderDetails orderDetails);

        void ConfirmOrder(int orderId, int customerId);

        void PaymentProcessed(int orderId, int customerId);

        void OrderSentToCustomer(int orderId, int customerId);
    }
}
