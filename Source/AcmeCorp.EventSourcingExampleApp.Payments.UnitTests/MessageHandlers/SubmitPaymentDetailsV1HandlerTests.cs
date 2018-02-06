namespace AcmeCorp.EventSourcingExampleApp.Payments.UnitTests.MessageHandlers
{
    using AcmeCorp.EventSourcingExampleApp.Contracts.Dto;
    using AcmeCorp.EventSourcingExampleApp.Contracts.Messages.Commands;
    using AcmeCorp.EventSourcingExampleApp.Payments.MessageHandlers;
    using Moq;
    using Xunit;

    public class SubmitPaymentDetailsV1HandlerTests
    {
        [Fact]
        public void Given_When_Then()
        {
            // Arrange
            const int orderId = 1;
            PaymentDetails paymentDetails = new PaymentDetails
            {
                PaymentId = 2,
                Amount = 9.99m,
                CardNumber = "1234123412341234",
                Ccv = "123",
                Expiry = "10/20"
            };
            Mock<IPaymentsDataStore> mockPaymentsDataStore = new Mock<IPaymentsDataStore>(MockBehavior.Strict);
            mockPaymentsDataStore
                .Setup(x => x.Save(orderId, paymentDetails));
            Mock<IApplicationLogger> mockLogger = new Mock<IApplicationLogger>(MockBehavior.Loose);
            IHandleMessage<SubmitPaymentDetailsV1> handler = new SubmitPaymentDetailsV1Handler(mockPaymentsDataStore.Object, mockLogger.Object);
            SubmitPaymentDetailsV1 message = new SubmitPaymentDetailsV1(orderId, paymentDetails);

            // Act
            handler.Handle(message);

            // Assert
            mockPaymentsDataStore.VerifyAll();
        }
    }
}
