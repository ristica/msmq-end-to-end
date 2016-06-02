namespace Msmq.OnlineShop.Messages.Workflows
{
    public class PackagingWorkflow
    {
        private string _product;

        public PackagingWorkflow(string product)
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
