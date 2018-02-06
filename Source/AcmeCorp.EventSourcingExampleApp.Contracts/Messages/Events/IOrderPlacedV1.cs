namespace AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;

    public interface IOrderPlacedV1
    {
        int CustomerId { get; }

        int OrderId { get; }

        OrderDetails OrderDetails { get; }
    }
}
