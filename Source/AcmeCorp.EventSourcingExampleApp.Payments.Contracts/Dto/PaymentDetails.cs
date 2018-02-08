namespace AcmeCorp.EventSourcingExampleApp.Payments.Contracts.Dto
{
    public class PaymentDetails
    {
        public int PaymentId { get; set; }

        public string CardNumber { get; set; }

        public string Expiry { get; set; }

        public string Ccv { get; set; }

        public decimal Amount { get; set; }
    }
}
