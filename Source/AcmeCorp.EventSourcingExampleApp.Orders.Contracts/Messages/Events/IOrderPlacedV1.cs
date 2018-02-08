namespace AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Events
{
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Dto;

    public interface IOrderPlacedV1
    {
        int CustomerId { get; }

        int OrderId { get; }

        OrderDetails OrderDetails { get; }
    }
}
