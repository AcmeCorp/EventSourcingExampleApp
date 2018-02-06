namespace AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;

    public interface IDeliveryOptionsSubmittedV1
    {
        int OrderId { get; }

        DeliveryOptions DeliveryOptions { get; }
    }
}
