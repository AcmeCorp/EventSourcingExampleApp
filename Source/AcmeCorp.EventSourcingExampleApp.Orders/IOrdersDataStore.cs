namespace AcmeCorp.EventSourcingExampleApp.Orders
{
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Dto;

    public interface IOrdersDataStore
    {
        void Save(int orderId, OrderDetails orderDetailsToSave);

        OrderDetails Load(int orderId);
    }
}
