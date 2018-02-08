namespace AcmeCorp.EventSourcingExampleApp.Payments.Contracts.Messages.Commands
{
    public class ProcessPaymentV1
    {
        public ProcessPaymentV1(int orderId, int customerId)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }

        public int OrderId { get; }

        public int CustomerId { get; }
    }
}
