using System.Messaging;
using Msmq.OnlineShop.Common.Extensions;
using Msmq.OnlineShop.Messages.Commands;
using Msmq.OnlineShop.ViewModels;

namespace Msmq.OnlineShop.ApplicationServices
{
    public class PlaceNewOrderService
    {
        private const string PlaceOrderQueue = @".\private$\place-order";

        public void PlaceOrder(ProductViewModel vm)
        {
            var command = new PlaceOrderCommand(vm.ProductId, vm.ProductName, vm.Quantity);

            using (var queue = new MessageQueue(PlaceOrderQueue))
            {
                var message = new Message
                {
                    BodyStream = command.ToJsonStream(),
                    Label = command.GetMessageType(),
                    Recoverable = true
                };

                queue.Send(message);
            }
        }
    }
}
