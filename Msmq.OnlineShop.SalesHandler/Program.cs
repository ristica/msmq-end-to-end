using Msmq.OnlineShop.Common.Extensions;
using System;
using System.Messaging;
using Msmq.OnlineShop.Messages.Commands;
using Msmq.OnlineShop.Messages.Workflows;

namespace Msmq.OnlineShop.SalesHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SALES";
            Console.WriteLine();
            const string placeOrderQueue = @".\private$\place-order-queue";

            using (var queue = new MessageQueue(placeOrderQueue))
            {
                while (true)
                {
                    Console.WriteLine("Listening for new events => PlaceOrderCommand");
                    Console.WriteLine();

                    var message = queue.Receive();
                    if (message == null) continue;
                    var messageBody = message.BodyStream.ReadFromJson(message.Label);

                    var messageType = messageBody.GetType();
                    if (messageType == typeof(PlaceOrderCommand))
                    {
                        UpdateStock((PlaceOrderCommand)messageBody);
                    }
                }
            }
        }

        private static void UpdateStock(PlaceOrderCommand messageBody)
        {
            Console.WriteLine("Starting UpdateStock for: {0}, at {1}", messageBody.Product, DateTime.Now);

            var workflow = new UpdateStockWorkflow(messageBody.Id, messageBody.Product, messageBody.Quantity);
            workflow.Run();

            Console.WriteLine("Finished UpdateStock for: {0}, at {1}", messageBody.Product, DateTime.Now);
            Console.WriteLine();
        }
    }
}
