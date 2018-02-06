namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain.Events
{
    public class OrderAcceptedAcknowledgedV1
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
    }
}
