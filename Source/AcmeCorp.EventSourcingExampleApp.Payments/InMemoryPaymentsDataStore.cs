namespace AcmeCorp.EventSourcingExampleApp.Payments
{
    using System.Collections.Generic;
    using AcmeCorp.EventSourcingExampleApp.Payments.Contracts.Dto;

    public class InMemoryPaymentsDataStore : IPaymentsDataStore
    {
        private readonly Dictionary<int, PaymentDetails> paymentDetails = new Dictionary<int, PaymentDetails>();

        public void Save(int orderId, PaymentDetails paymentDetailsToSave)
        {
            this.paymentDetails.Add(orderId, paymentDetailsToSave);
        }

        public bool CheckExists(int orderId)
        {
            return this.paymentDetails.ContainsKey(orderId);
        }

        public PaymentDetails Load(int orderId)
        {
            if (!this.CheckExists(orderId))
            {
                throw new EventSourcingExampleAppException("Could not find payment details for that Order ID.");
            }

            return this.paymentDetails[orderId];
        }
    }
}
