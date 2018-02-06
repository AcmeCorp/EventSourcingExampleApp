namespace AcmeCorp.EventSourcingExampleApp.Console
{
    using System;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.ServiceBus;

    public static class Program
    {
        private static void Main()
        {
            IBus bus = new FakeBus();
            IApplicationLogger applicationLogger = new ConsoleApplicationLogger();

            const int orderId = 100;
            const int customerId = 200;
            const int paymentId = 300;

            // Place order
            PlaceOrderV1 placeOrderV1 = new PlaceOrderV1(orderId, customerId);
            placeOrderV1.OrderDetails.OrderItems.Add(new OrderItem(400, "desc", 9.99m, 2));
            applicationLogger.SendMessage(placeOrderV1);
            bus.Send(placeOrderV1);

            // Choose delivery options
            SubmitDeliveryOptionsV1 submitDeliveryOptionsV1 = new SubmitDeliveryOptionsV1(orderId);
            submitDeliveryOptionsV1.DeliveryOptions.DeliveryMethod = "method";
            submitDeliveryOptionsV1.DeliveryOptions.Address.Line1 = "Line 1";
            submitDeliveryOptionsV1.DeliveryOptions.Address.Line2 = "Line 2";
            submitDeliveryOptionsV1.DeliveryOptions.Address.City = "City";
            submitDeliveryOptionsV1.DeliveryOptions.Address.PostCode = "A1 1AB";
            applicationLogger.SendMessage(submitDeliveryOptionsV1);
            bus.Send(submitDeliveryOptionsV1);

            // Submit payment info
            SubmitPaymentDetailsV1 submitPaymentDetailsV1 = new SubmitPaymentDetailsV1(orderId);
            submitPaymentDetailsV1.PaymentDetails.Amount = 9.99m;
            submitPaymentDetailsV1.PaymentDetails.CardNumber = "1234123412341234";
            submitPaymentDetailsV1.PaymentDetails.Ccv = "111";
            submitPaymentDetailsV1.PaymentDetails.Expiry = "01/01";
            submitPaymentDetailsV1.PaymentDetails.PaymentId = paymentId;
            applicationLogger.SendMessage(submitPaymentDetailsV1);
            bus.Send(submitPaymentDetailsV1);

            // Confirm order
            ConfirmOrderV1 confirmOrderV1 = new ConfirmOrderV1(orderId, customerId);
            applicationLogger.SendMessage(confirmOrderV1);
            bus.Send(confirmOrderV1);

            Console.ReadLine();
        }
    }
}
