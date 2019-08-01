using System;
using System.Messaging;
using Msmq.OnlineShop.Common.Extensions;
using Msmq.OnlineShop.Messages.Events;
using Msmq.OnlineShop.Messages.Workflows;

namespace Msmq.OnlineShop.ShippingHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SHIPPING";
            Console.WriteLine();
            const string queueAddress = @".\private$\shipping-queue";

            using (var queue = new MessageQueue(queueAddress))
            {
                queue.MulticastAddress = "234.1.1.1:8001";
                while (true)
                {
                    Console.WriteLine("Listening for events => PlaceOrderEvent...");
                    Console.WriteLine();

                    var message = queue.Receive();
                    if (message == null) continue;
                    var body = message.BodyStream.ReadFromJson(message.Label);
                    if (body.GetType() != typeof(OrderPlacedEvent)) continue;
                    var @event = body as OrderPlacedEvent;
                    Console.WriteLine("\tReceived: {0}, at {1}", @event.Product, DateTime.Now);

                    var workflow = new ShippingWorkflow(@event.Product);
                    workflow.Run();

                    Console.WriteLine("\tProcessed...");
                    Console.WriteLine();
                }
            }
        }
    }
}
