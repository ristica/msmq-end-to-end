using System;
using System.Diagnostics;
using Msmq.OnlineShop.Messages.Events;
using System.Messaging;
using Msmq.OnlineShop.Common.Extensions;

namespace Msmq.OnlineShop.Messages.Workflows
{
    public class UpdateStockWorkflow
    {
        private int _id;
        private readonly string _product;
        private int _quantity;

        public UpdateStockWorkflow(int id, string product, int quantity)
        {
            this._id = id;
            this._product = product;
            this._quantity = quantity;
        }

        public void Run()
        {
            PersistStock();
            NotifySubscriber();
        }

        private void PersistStock()
        {
            // here we would make some db operations ao stock
            // ...
        }

        private void NotifySubscriber()
        {
            var orderPlacedEvent = new OrderPlacedEvent
            {
                Product = this._product
            };

            try
            {
                using (var queue = new MessageQueue("FormatName:MULTICAST=234.1.1.1:8001"))
                {
                    var message = new Message
                    {
                        BodyStream = orderPlacedEvent.ToJsonStream(),
                        Label = orderPlacedEvent.GetMessageType(),
                        Recoverable = true
                    };
                    queue.Send(message);
                }
            }
            catch (Exception)
            {
                Debugger.Break();
            }
        }
    }
}
