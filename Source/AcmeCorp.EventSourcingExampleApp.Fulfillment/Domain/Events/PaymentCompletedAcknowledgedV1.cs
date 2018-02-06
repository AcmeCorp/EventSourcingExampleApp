namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain.Events
{
    public class PaymentCompletedAcknowledgedV1
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
    }
}
