namespace AcmeCorp.EventSourcingExampleApp.Fulfillment
{
    using System.Collections.Generic;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Dto;

    public class InMemoryDeliveryOptionsDataStore : IDeliveryOptionsDataStore
    {
        private readonly Dictionary<int, DeliveryOptions> deliveryOptions = new Dictionary<int, DeliveryOptions>();

        public void Save(int orderId, DeliveryOptions deliveryOptionsToSave)
        {
            this.deliveryOptions.Add(orderId, deliveryOptionsToSave);
        }

        public DeliveryOptions Load(int orderId)
        {
            if (!this.deliveryOptions.ContainsKey(orderId))
            {
                throw new EventSourcingExampleAppException("Could not find order details for that Order ID.");
            }

            return this.deliveryOptions[orderId];
        }
    }
}