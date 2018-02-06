namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain.Events;

    public class DeliveryAggregate :
        Aggregate,
        IDeliveryAggregate,
        IHandleEvent<DeliveryOptionsSubmittedV1>,
        IHandleEvent<OrderAcceptedAcknowledgedV1>,
        IHandleEvent<PaymentCompletedAcknowledgedV1>
    {
        private bool deliveryOptionsSubmitted;

        private bool orderAccepted;

        private bool paymentCompleted;

        public DeliveryAggregate(string eventStreamId)
            : base(eventStreamId)
        {
        }

        public void SubmitDeliveryOptions(int orderId, DeliveryOptions deliveryOptions)
        {
            if (!this.deliveryOptionsSubmitted)
            {
                if (deliveryOptions == null ||
                    deliveryOptions.Address == null ||
                    string.IsNullOrEmpty(deliveryOptions.DeliveryMethod))
                {
                    throw new EventSourcingExampleAppException("Invalid delivery options.");
                }

                IDeliveryOptionsSubmittedV1 deliveryOptionsSubmittedV1 = new DeliveryOptionsSubmittedV1(orderId);
                deliveryOptionsSubmittedV1.DeliveryOptions.DeliveryMethod = deliveryOptions.DeliveryMethod;
                deliveryOptionsSubmittedV1.DeliveryOptions.Address.Line1 = deliveryOptions.Address.Line1;
                deliveryOptionsSubmittedV1.DeliveryOptions.Address.Line2 = deliveryOptions.Address.Line2;
                deliveryOptionsSubmittedV1.DeliveryOptions.Address.City = deliveryOptions.Address.City;
                deliveryOptionsSubmittedV1.DeliveryOptions.Address.PostCode = deliveryOptions.Address.PostCode;
                this.Apply(deliveryOptionsSubmittedV1);
            }
        }

        public void AcceptOrder(int orderId)
        {
            if (!this.orderAccepted)
            {
                OrderAcceptedAcknowledgedV1 message = new OrderAcceptedAcknowledgedV1 { OrderId = orderId };
                this.Apply(message);
            }
        }

        public void CompletePayment(int orderId)
        {
            if (!this.paymentCompleted)
            {
                PaymentCompletedAcknowledgedV1 message = new PaymentCompletedAcknowledgedV1 { OrderId = orderId };
                this.Apply(message);
            }
        }

        public void Handle(DeliveryOptionsSubmittedV1 message)
        {
            this.deliveryOptionsSubmitted = true;
        }

        public void Handle(OrderAcceptedAcknowledgedV1 message)
        {
            this.orderAccepted = true;
            this.CheckForDispatch(message.OrderId, message.CustomerId);
        }

        public void Handle(PaymentCompletedAcknowledgedV1 message)
        {
            this.paymentCompleted = true;
            this.CheckForDispatch(message.OrderId, message.CustomerId);
        }

        private void CheckForDispatch(int orderId, int customerId)
        {
            if (this.orderAccepted && this.paymentCompleted)
            {
                // Some logic here to deal with the dispatch of the order, this would
                // likely involve a sequence of events that end with "OrderFulfilledV1".
                // So this aggregate would probably raise some event (e.g. "Order Ready
                // For Dispatch") which would be acted on by some service. This aggregate
                // would then pick up some resulting event saying the order had been
                // dispatched. This would ultimately result in the "OrderFulfilledV1" event.
                IOrderFulfilledV1 orderFulfilledV1 = new OrderFulfilledV1(orderId, customerId);
                this.Apply(orderFulfilledV1);
            }
        }
    }
}
