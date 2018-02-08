namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Messages.Events
{
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Dto;

    public class DeliveryOptionsSubmittedV1 : IDeliveryOptionsSubmittedV1
    {
        public DeliveryOptionsSubmittedV1(int orderId)
        {
            this.OrderId = orderId;
            this.DeliveryOptions = new DeliveryOptions();
        }

        public int OrderId { get; }

        public DeliveryOptions DeliveryOptions { get; }
    }
}