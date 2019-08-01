using System;
using System.Messaging;
using Msmq.OnlineShop.Common.Extensions;
using Msmq.OnlineShop.Messages.Queries;

namespace Msmq.OnlineShop.InventoryHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "PRODUCT IN STOCK";
            Console.WriteLine();
            const string queueAddress = @".\private$\product-in-stock-queue";

            using (var queue = new MessageQueue(queueAddress))
            {
                while (true)
                {
                    Console.WriteLine("Listening for new events => ProductInStockRequest");
                    Console.WriteLine();

                    var message = queue.Receive();
                    if (message == null) continue;
                    var messageBody = message.BodyStream.ReadFromJson(message.Label);

                    var messageType = messageBody.GetType();
                    if (messageType == typeof(ProductInStockRequest))
                    {
                        CheckIfProductInStock(message.ResponseQueue, (ProductInStockRequest)messageBody);
                    }
                }
            }
        }

        private static void CheckIfProductInStock(MessageQueue responseQueue, ProductInStockRequest request)
        {
            Console.WriteLine("\tchecking if product with the id '" + request.ProductId + "' is in stock");

            // get stock...
            var response = new ProductInStockResponse
            {
                Stock = 8
            };

            // send response back
            using (var queue = responseQueue)
            {
                var message = new Message
                {
                    BodyStream = response.ToJsonStream(),
                    Label = responseQueue.GetMessageType()
                };
                queue.Send(message);
            }

            Console.WriteLine("\tcurrently are '" + response.Stock + "' in stock");
            Console.WriteLine("\tstock sent...");
            Console.WriteLine();
        }
    }
}
