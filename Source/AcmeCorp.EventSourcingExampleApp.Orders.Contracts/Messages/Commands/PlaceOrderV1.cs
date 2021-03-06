﻿namespace AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Messages.Commands
{
    using AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Dto;

    public class PlaceOrderV1
    {
        public PlaceOrderV1(int orderId, int customerId)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.OrderDetails = new OrderDetails();
        }

        public int OrderId { get; }

        public int CustomerId { get; }

        public OrderDetails OrderDetails { get; }
    }
}
