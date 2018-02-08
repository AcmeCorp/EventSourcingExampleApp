namespace AcmeCorp.EventSourcingExampleApp.Orders.Contracts.Dto
{
    public class OrderItem
    {
        public OrderItem(int productId, string productDescription, decimal productPrice, int quantity)
        {
            this.ProductId = productId;
            this.ProductDescription = productDescription;
            this.ProductPrice = productPrice;
            this.Quantity = quantity;
        }

        public int ProductId { get; }

        public string ProductDescription { get; }

        public decimal ProductPrice { get; }

        public int Quantity { get; }
    }
}
