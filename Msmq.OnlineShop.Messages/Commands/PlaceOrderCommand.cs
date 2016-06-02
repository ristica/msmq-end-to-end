namespace Msmq.OnlineShop.Messages.Commands
{
    public class PlaceOrderCommand
    {
        public int Id { get; private set; }
        public string Product { get; private set; }
        public int Quantity { get; private set; }

        public PlaceOrderCommand(int id, string product, int quantity)
        {
            this.Id = id;
            this.Product = product;
            this.Quantity = quantity;
        }
    }
}
