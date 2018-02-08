namespace AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Events
{
    public interface IOrderAcceptedV1
    {
        int CustomerId { get; }

        int OrderId { get; }
    }
}