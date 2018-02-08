namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Messages.Commands
{
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Dto;

    public class SubmitDeliveryOptionsV1
    {
        public SubmitDeliveryOptionsV1(int orderId)
        {
            this.OrderId = orderId;
            this.DeliveryOptions = new DeliveryOptions();
        }

        public int OrderId { get; }

        public DeliveryOptions DeliveryOptions { get; }
    }
}
