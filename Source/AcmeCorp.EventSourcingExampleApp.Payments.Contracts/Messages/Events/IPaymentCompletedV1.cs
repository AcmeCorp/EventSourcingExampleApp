﻿namespace AcmeCorp.EventSourcingExampleApp.Payments.Contracts.Messages.Events
{
    public interface IPaymentCompletedV1
    {
        int CustomerId { get; }

        int OrderId { get; }

        int PaymentId { get; }
    }
}