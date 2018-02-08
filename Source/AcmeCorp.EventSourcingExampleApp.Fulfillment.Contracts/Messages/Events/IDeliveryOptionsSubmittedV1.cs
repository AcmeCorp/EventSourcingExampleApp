namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Messages.Events
{
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Dto;

    public interface IDeliveryOptionsSubmittedV1
    {
        int OrderId { get; }

        DeliveryOptions DeliveryOptions { get; }
    }
}
