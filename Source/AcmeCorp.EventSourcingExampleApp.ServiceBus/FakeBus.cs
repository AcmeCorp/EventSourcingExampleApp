namespace AcmeCorp.EventSourcingExampleApp.ServiceBus
{
    using System;
    using System.Threading;
    using AcmeCorp.EventSourcing;
    using AcmeCorp.EventSourcing.Logging;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain;
    using AcmeCorp.EventSourcingExampleApp.Orders;
    using AcmeCorp.EventSourcingExampleApp.Orders.Domain;
    using AcmeCorp.EventSourcingExampleApp.Payments;

    /// <remarks>
    /// A fake service bus.
    /// Just a class that accepts messages and directly invokes the respective message handler(s).
    /// A "real" bus would obviously route messages and write them to message queues and the
    /// handlers (running in separate processes) would pick up the messages from their own queues.
    /// This is just a hard coded "bus" for demo purposes.
    /// </remarks>
    public class FakeBus : IBus
    {
        private const int SleepTime = 1000;
        private readonly ILogger logger;
        private readonly IApplicationLogger applicationLogger;
        private readonly IOrderAggregateFactory orderAggregateFactory;
        private readonly IDeliveryAggregateFactory deliveryAggregateFactory;
        private readonly IOrdersDataStore ordersDataStore;
        private readonly IDeliveryOptionsDataStore deliveryOptionsDataStore;
        private readonly IPaymentsDataStore paymentsDataStore;

        public FakeBus()
        {
            this.logger = new ConsoleLogger();
            this.applicationLogger = new ConsoleApplicationLogger();
            this.paymentsDataStore = new InMemoryPaymentsDataStore();
            this.orderAggregateFactory = new OrderAggregateFactory();
            this.deliveryAggregateFactory = new DeliveryAggregateFactory();
            this.ordersDataStore = new InMemoryOrdersDataStore();
            this.deliveryOptionsDataStore = new InMemoryDeliveryOptionsDataStore();
        }

        /// <remarks>
        /// Some hardcoded routing.
        /// </remarks>
        public void Publish(object message)
        {
            if (message is IOrderAcceptedV1 orderAcceptedV1)
            {
                IHandleMessage<IOrderAcceptedV1> fulfillmentOrderAcceptedHandler = new Fulfillment.MessageHandlers.OrderAcceptedV1Handler(this.CreateDomainRepository(), this.deliveryAggregateFactory, this.applicationLogger);
                fulfillmentOrderAcceptedHandler.Handle(orderAcceptedV1);
                Thread.Sleep(SleepTime);
                IHandleMessage<IOrderAcceptedV1> paymentsOrderAcceptedHandler = new Payments.MessageHandlers.OrderAcceptedV1Handler(this, this.paymentsDataStore, this.applicationLogger);
                paymentsOrderAcceptedHandler.Handle(orderAcceptedV1);
            }
            else if (message is IOrderFulfilledV1 orderFulfilledV1)
            {
                IHandleMessage<IOrderFulfilledV1> ordersOrderFulfilledHandler = new Orders.MessageHandlers.OrderFulfilledV1Handler(this.CreateDomainRepository(), this.orderAggregateFactory, this.applicationLogger);
                ordersOrderFulfilledHandler.Handle(orderFulfilledV1);
            }
            else if (message is IPaymentCompletedV1 paymentCompletedV1)
            {
                IHandleMessage<IPaymentCompletedV1> fulfillmentPaymentCompletedHandler = new Fulfillment.MessageHandlers.PaymentCompletedV1Handler(this.CreateDomainRepository(), this.deliveryAggregateFactory, this.applicationLogger);
                fulfillmentPaymentCompletedHandler.Handle(paymentCompletedV1);
                Thread.Sleep(SleepTime);
                IHandleMessage<IPaymentCompletedV1> ordersPaymentCompletedHandler = new Orders.MessageHandlers.PaymentCompletedV1Handler(this.CreateDomainRepository(), this.orderAggregateFactory, this.applicationLogger);
                ordersPaymentCompletedHandler.Handle(paymentCompletedV1);
            }
            else if (message is IOrderPlacedV1 orderPlacedV1)
            {
                IHandleMessage<IOrderPlacedV1> handler = new Orders.MessageHandlers.OrderPlacedV1Handler(this.ordersDataStore, this.applicationLogger);
                handler.Handle(orderPlacedV1);
            }
            else if (message is IDeliveryOptionsSubmittedV1 deliveryOptionsSubmittedV1)
            {
                IHandleMessage<IDeliveryOptionsSubmittedV1> handler = new Fulfillment.MessageHandlers.DeliveryOptionsSubmittedV1Handler(this.deliveryOptionsDataStore, this.applicationLogger);
                handler.Handle(deliveryOptionsSubmittedV1);
            }
            else
            {
                this.applicationLogger.Warn($"No handlers for message type '{message.GetType().Name}' (it may be an internal domain event).");
            }
        }

        /// <remarks>
        /// Some hardcoded routing.
        /// </remarks>
        public void Send(object message)
        {
            Type messageType = message.GetType();
            if (messageType == typeof(ConfirmOrderV1))
            {
                IHandleMessage<ConfirmOrderV1> handler = new Orders.MessageHandlers.ConfirmOrderV1Handler(this.CreateDomainRepository(), this.orderAggregateFactory, this.applicationLogger);
                handler.Handle((ConfirmOrderV1)message);
            }
            else if (messageType == typeof(PlaceOrderV1))
            {
                IHandleMessage<PlaceOrderV1> handler = new Orders.MessageHandlers.PlaceOrderV1Handler(this.CreateDomainRepository(), this.orderAggregateFactory, this.applicationLogger);
                handler.Handle((PlaceOrderV1)message);
            }
            else if (messageType == typeof(SubmitDeliveryOptionsV1))
            {
                IHandleMessage<SubmitDeliveryOptionsV1> handler = new Fulfillment.MessageHandlers.SubmitDeliveryOptionsV1Handler(this.CreateDomainRepository(), this.deliveryAggregateFactory, this.applicationLogger);
                handler.Handle((SubmitDeliveryOptionsV1)message);
            }
            else if (messageType == typeof(SubmitPaymentDetailsV1))
            {
                IHandleMessage<SubmitPaymentDetailsV1> handler = new Payments.MessageHandlers.SubmitPaymentDetailsV1Handler(this.paymentsDataStore, this.applicationLogger);
                handler.Handle((SubmitPaymentDetailsV1)message);
            }
            else if (messageType == typeof(ProcessPaymentV1))
            {
                IHandleMessage<ProcessPaymentV1> handler = new Payments.MessageHandlers.ProcessPaymentV1Handler(this, this.paymentsDataStore, new CreditCardPaymentProvider(this.applicationLogger), this.applicationLogger);
                handler.Handle((ProcessPaymentV1)message);
            }
            else
            {
                throw new NotSupportedException($"No handlers configured for the command '{message.GetType().Name}'.");
            }
        }

        private IDomainRepository CreateDomainRepository()
        {
            return new EventStoreDomainRepository(new InMemoryEventStoreWithDispatcherProvider(this, this.applicationLogger, this.logger));
        }
    }
}