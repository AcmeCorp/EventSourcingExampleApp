namespace AcmeCorp.EventSourcingExampleApp.Payments.UnitTests.MessageHandlers
{
    using System;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Events;
    using AcmeCorp.EventSourcingExampleApp.Payments.MessageHandlers;
    using Moq;
    using Xunit;

    public class OrderAcceptedV1HandlerTests
    {
        [Fact]
        public void Given_An_Order_Accepted_Event_When_Valid_Payment_Details_Have_Been_Recorded_Then_The_Process_Payment_Command_Is_Sent()
        {
            // Arrange
            const int orderId = 1;
            const int customerId = 2;
            const int paymentId = 3;
            PaymentDetails paymentDetails = new PaymentDetails { PaymentId = paymentId };
            Mock<IBus> mockBus = new Mock<IBus>(MockBehavior.Strict);
            mockBus.Setup(b => b.Send(It.Is<ProcessPaymentV1>(pc =>
                pc.OrderId == orderId &&
                pc.CustomerId == customerId)));
            Mock<IPaymentsDataStore> mockPaymentsDataStore = new Mock<IPaymentsDataStore>(MockBehavior.Strict);
            mockPaymentsDataStore
                .Setup(x => x.CheckExists(orderId))
                .Returns(true);
            Mock<IApplicationLogger> mockLogger = new Mock<IApplicationLogger>(MockBehavior.Loose);
            IHandleMessage<IOrderAcceptedV1> handler = new OrderAcceptedV1Handler(mockBus.Object, mockPaymentsDataStore.Object, mockLogger.Object);
            IOrderAcceptedV1 message = new OrderAcceptedV1(customerId, orderId);

            // Act
            handler.Handle(message);

            // Assert
            mockBus.VerifyAll();
            mockPaymentsDataStore.VerifyAll();
        }
    }
}
