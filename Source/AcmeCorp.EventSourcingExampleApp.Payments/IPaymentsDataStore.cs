namespace AcmeCorp.EventSourcingExampleApp.Payments
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;

    public interface IPaymentsDataStore
    {
        void Save(int orderId, PaymentDetails paymentDetailsToSave);

        bool CheckExists(int orderId);

        PaymentDetails Load(int orderId);
    }
}
