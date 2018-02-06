namespace AcmeCorp.EventSourcingExampleApp.Payments
{
    public interface ICreditCardPaymentProvider
    {
        void ProcessPayment(int paymentId, string cardNumber, string expiry, string ccv, decimal amount);
    }
}
