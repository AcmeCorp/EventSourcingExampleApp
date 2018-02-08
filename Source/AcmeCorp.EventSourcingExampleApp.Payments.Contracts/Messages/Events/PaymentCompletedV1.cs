namespace AcmeCorp.EventSourcingExampleApp.Payments.Contracts.Messages.Events
{
    public class PaymentCompletedV1 : IPaymentCompletedV1
    {
        public PaymentCompletedV1(int customerId, int orderId, int paymentId)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
            this.PaymentId = paymentId;
        }

        public int CustomerId { get; }

        public int OrderId { get; }

        public int PaymentId { get; }
    }
}