namespace AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events
{
    public interface IOrderCompletedV1
    {
        int CustomerId { get; }

        int OrderId { get; }
    }
}