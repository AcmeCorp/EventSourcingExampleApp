namespace AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Dto
{
    using System.Collections.Generic;

    public class OrderDetails
    {
        public OrderDetails()
        {
            this.OrderItems = new List<OrderItem>();
        }

        public IList<OrderItem> OrderItems { get; }

        public int OrderTotal { get; set; }
    }
}
