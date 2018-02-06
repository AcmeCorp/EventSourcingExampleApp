namespace AcmeCorp.EventSourcingExampleApp.Orders
{
    using System.Collections.Generic;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;

    public class InMemoryOrdersDataStore : IOrdersDataStore
    {
        private readonly Dictionary<int, OrderDetails> orderDetails = new Dictionary<int, OrderDetails>();

        public void Save(int orderId, OrderDetails orderDetailsToSave)
        {
            this.orderDetails.Add(orderId, orderDetailsToSave);
        }

        public OrderDetails Load(int orderId)
        {
            if (!this.orderDetails.ContainsKey(orderId))
            {
                throw new EventSourcingExampleAppException("Could not find order details for that Order ID.");
            }

            return this.orderDetails[orderId];
        }
    }
}