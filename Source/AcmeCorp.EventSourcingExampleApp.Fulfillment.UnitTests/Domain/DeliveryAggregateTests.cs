namespace AcmeCorp.EventSourcingExampleApp.Fulfillment.UnitTests.Domain
{
    using System.Globalization;
    using AcmeCorp.EventSourcing.Testing;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Fulfillment.Domain;
    using Xunit;

    public class DeliveryAggregateTests
    {
        [Fact]
        public void Given_Valid_Delivery_Options_When_The_Order_Is_Accepted_And_The_Payment_Is_Completed_Then_The_Order_Is_Fulfilled()
        {
            // Arrange
            const int orderId = 1234;
            DeliveryOptions deliveryOptions = new DeliveryOptions();
            deliveryOptions.DeliveryMethod = "some method";
            deliveryOptions.Address.Line1 = "Line 1";
            deliveryOptions.Address.Line2 = "Line 2";
            deliveryOptions.Address.City = "City";
            deliveryOptions.Address.PostCode = "A1 1AB";
            IDeliveryAggregate deliveryAggregate = new DeliveryAggregate(orderId.ToString(CultureInfo.InvariantCulture));

            // Act
            deliveryAggregate.SubmitDeliveryOptions(orderId, deliveryOptions);
            deliveryAggregate.AcceptOrder(orderId);
            deliveryAggregate.CompletePayment(orderId);

            // Assert
            deliveryAggregate.UncommittedEvents.LastEventIs(typeof(IOrderFulfilledV1));
        }
    }
}
