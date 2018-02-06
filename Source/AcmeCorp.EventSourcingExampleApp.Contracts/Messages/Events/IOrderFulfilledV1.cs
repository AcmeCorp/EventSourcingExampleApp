namespace AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events
{
    public interface IOrderFulfilledV1
    {
        int OrderId { get; }

        int CustomerId { get; }
    }
}