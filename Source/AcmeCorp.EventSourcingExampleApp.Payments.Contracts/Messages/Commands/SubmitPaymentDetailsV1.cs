﻿namespace AcmeCorp.EventSourcingExampleApp.Payments.Contracts.Messages.Commands
{
    using AcmeCorp.EventSourcingExampleApp.Payments.Contracts.Dto;

    public class SubmitPaymentDetailsV1
    {
        public SubmitPaymentDetailsV1(int orderId)
            : this(orderId, new PaymentDetails())
        {
        }

        public SubmitPaymentDetailsV1(int orderId, PaymentDetails paymentDetails)
        {
            this.OrderId = orderId;
            this.PaymentDetails = paymentDetails;
        }

        public int OrderId { get; }

        public PaymentDetails PaymentDetails { get; }
    }
}
