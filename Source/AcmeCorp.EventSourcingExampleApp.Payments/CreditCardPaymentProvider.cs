namespace AcmeCorp.EventSourcingExampleApp.Payments
{
    using System.Threading;

    public class CreditCardPaymentProvider : ICreditCardPaymentProvider
    {
        private readonly IApplicationLogger applicationLogger;

        public CreditCardPaymentProvider(IApplicationLogger applicationLogger)
        {
            this.applicationLogger = applicationLogger;
        }

        public void ProcessPayment(int paymentId, string cardNumber, string expiry, string ccv, decimal amount)
        {
            this.applicationLogger.Info("Processing payment...");
            Thread.Sleep(1000);
            this.applicationLogger.Info("...payment processed.");
        }
    }
}