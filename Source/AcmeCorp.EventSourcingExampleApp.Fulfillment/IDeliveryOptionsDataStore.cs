namespace AcmeCorp.EventSourcingExampleApp.Fulfillment
{
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Dto;

    public interface IDeliveryOptionsDataStore
    {
        void Save(int orderId, DeliveryOptions deliveryOptionsToSave);

        DeliveryOptions Load(int orderId);
    }
}
