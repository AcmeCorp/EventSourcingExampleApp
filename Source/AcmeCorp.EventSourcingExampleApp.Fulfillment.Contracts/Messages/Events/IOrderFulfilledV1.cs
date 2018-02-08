namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Messages.Events
{
    public interface IOrderFulfilledV1
    {
        int OrderId { get; }

        int CustomerId { get; }
    }
}