namespace AcmeCorp.EventSourcingExampleApp.Orders.UnitTests.Domain
{
    using System.Globalization;
    using AcmeCorp.EventSourcing.Testing;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Orders.Domain;
    using Xunit;

    public class OrderAggregateTests
    {
        [Fact]
        public void Given_A_Valid_Order_When_The_Order_Is_Received_Then_The_Order_Is_Placed()
        {
            // Arrange
            const int orderId = 1234;
            const int customerId = 5678;
            OrderDetails orderDetails = new OrderDetails();
            orderDetails.OrderItems.Add(new OrderItem(1, "desc", 9.99m, 2));
            IOrderAggregate orderAggregate = new OrderAggregate(orderId.ToString(CultureInfo.InvariantCulture));

            // Act
            orderAggregate.PlaceOrder(orderId, customerId, orderDetails);

            // Assert
            Assert.Equal(1, orderAggregate.UncommittedEvents.Count);
            Assert.True(orderAggregate.UncommittedEvents.FirstEventIs(typeof(IOrderPlacedV1)));
        }

        [Fact]
        public void Given_An_Order_Has_Been_Placed_When_The_Order_Is_Confirmed_Then_The_Order_Is_Accepted()
        {
            // Arrange
            const int orderId = 1234;
            const int customerId = 5678;
            OrderDetails orderDetails = new OrderDetails();
            orderDetails.OrderItems.Add(new OrderItem(1, "desc", 9.99m, 2));
            IOrderAggregate orderAggregate = new OrderAggregate(orderId.ToString(CultureInfo.InvariantCulture));

            // Act
            orderAggregate.PlaceOrder(orderId, customerId, orderDetails);
            orderAggregate.ConfirmOrder(orderId, customerId);

            // Assert
            Assert.Equal(2, orderAggregate.UncommittedEvents.Count);
            Assert.True(orderAggregate.UncommittedEvents.LastEventIs(typeof(IOrderAcceptedV1)));
        }

        [Fact]
        public void Given_An_Order_That_Has_Been_Accepted_When_The_Payment_Is_Processed_And_The_Order_Fulfilled_Then_The_Order_Is_Complete()
        {
            // Arrange
            const int orderId = 1234;
            const int customerId = 5678;
            OrderDetails orderDetails = new OrderDetails();
            orderDetails.OrderItems.Add(new OrderItem(1, "desc", 9.99m, 2));
            IOrderAggregate orderAggregate = new OrderAggregate(orderId.ToString(CultureInfo.InvariantCulture));

            // Act
            orderAggregate.PlaceOrder(orderId, customerId, orderDetails);
            orderAggregate.ConfirmOrder(orderId, customerId);
            orderAggregate.PaymentProcessed(orderId, customerId);
            orderAggregate.OrderSentToCustomer(orderId, customerId);

            // Assert
            Assert.True(orderAggregate.OrderComplete);
        }
    }
}
