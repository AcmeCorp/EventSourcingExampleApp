namespace AcmeCorp.EventSourcingExampleApp.Orders.Domain
{
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcingExampleApp.Console.Messages.Domain.Events;
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Orders.Domain.Events;

    public class OrderAggregate :
        Aggregate,
        IOrderAggregate,
        IHandleEvent<OrderPlacedV1>,
        IHandleEvent<OrderAcceptedV1>,
        IHandleEvent<PaymentCompletedAcknowledgedV1>,
        IHandleEvent<OrderFulfilledAcknowledgedV1>,
        IHandleEvent<OrderCompletedV1>
    {
        private bool orderPlaced;

        private bool orderAccepted;

        private bool paymentCompleted;

        private bool orderFulfilled;

        public OrderAggregate(string eventStreamId)
            : base(eventStreamId)
        {
        }

        public bool OrderComplete { get; private set; }

        public void PlaceOrder(int orderId, int customerId, OrderDetails orderDetails)
        {
            // Guards against duplicate messages - check if this order has already been placed.
            if (!this.orderPlaced)
            {
                // Validate the order.
                if (orderDetails.OrderItems.Count < 1)
                {
                    throw new EventSourcingExampleAppException("There must be one or more order items.");
                }

                // Create an event.
                IOrderPlacedV1 orderPlacedV1 = new OrderPlacedV1(orderId, customerId);
                orderPlacedV1.OrderDetails.OrderTotal = orderDetails.OrderTotal;
                foreach (OrderItem orderItem in orderDetails.OrderItems)
                {
                    orderPlacedV1.OrderDetails.OrderItems.Add(orderItem);
                }

                // Publish the event (this only happens if the order has not already been placed - see guard clause above).
                this.Apply(orderPlacedV1);
            }
        }

        public void ConfirmOrder(int orderId, int customerId)
        {
            // Guards against duplicate messages - check if this order has already been accepted.
            if (!this.orderAccepted)
            {
                // There could be additional validation here. For example, orders must be confirmed
                // within x time from being "placed". The actual content of the order was validated
                // when it was placed.
                if (!this.orderPlaced)
                {
                    throw new EventSourcingExampleAppException("Can't confirm an order that has not been placed.");
                }

                IOrderAcceptedV1 orderAcceptedV1 = new OrderAcceptedV1(customerId, orderId);
                this.Apply(orderAcceptedV1);
            }
        }

        public void PaymentProcessed(int orderId, int customerId)
        {
            if (!this.paymentCompleted)
            {
                this.Apply(new PaymentCompletedAcknowledgedV1());
                this.OrderCompleteCheck(customerId, orderId);
            }
        }

        public void OrderSentToCustomer(int orderId, int customerId)
        {
            if (!this.orderFulfilled)
            {
                this.Apply(new OrderFulfilledAcknowledgedV1());
                this.OrderCompleteCheck(customerId, orderId);
            }
        }

        public void Handle(OrderPlacedV1 message)
        {
            this.orderPlaced = true;
        }

        public void Handle(OrderAcceptedV1 message)
        {
            this.orderAccepted = true;
        }

        public void Handle(PaymentCompletedAcknowledgedV1 message)
        {
            this.paymentCompleted = true;
        }

        public void Handle(OrderFulfilledAcknowledgedV1 message)
        {
            this.orderFulfilled = true;
        }

        public void Handle(OrderCompletedV1 message)
        {
            this.OrderComplete = true;
        }

        private void OrderCompleteCheck(int customerId, int orderId)
        {
            if (this.paymentCompleted && this.orderFulfilled)
            {
                IOrderCompletedV1 orderCompletedV1 = new OrderCompletedV1(customerId, orderId);
                this.Apply(orderCompletedV1);
            }
        }
    }
}
