namespace AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands
{
    public class ConfirmOrderV1
    {
        public ConfirmOrderV1(int orderId, int customerId)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }

        public int OrderId { get; }

        public int CustomerId { get; }
    }
}
