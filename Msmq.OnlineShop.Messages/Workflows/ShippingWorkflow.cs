namespace Msmq.OnlineShop.Messages.Workflows
{
    public class ShippingWorkflow
    {
        private string _product;

        public ShippingWorkflow(string product)
        {
            this._product = product;
        }

        public void Run()
        {
            // here we do some routine
            // ...
        }
    }
}
