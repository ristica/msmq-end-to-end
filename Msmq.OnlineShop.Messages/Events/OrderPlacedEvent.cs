namespace Msmq.OnlineShop.Messages.Events
{
    public class OrderPlacedEvent
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
    }
}
