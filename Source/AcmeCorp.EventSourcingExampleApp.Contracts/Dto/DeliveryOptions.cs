namespace AcmeCorp.EventSourcingExampleApp.Contracts.Dto
{
    public class DeliveryOptions
    {
        public DeliveryOptions()
        {
            this.Address = new Address();
        }

        public string DeliveryMethod { get; set; }

        public Address Address { get; }
    }
}
