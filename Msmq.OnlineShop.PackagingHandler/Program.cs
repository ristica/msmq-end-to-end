using Msmq.OnlineShop.Messages.Events;
using System;
using System.Messaging;
using Msmq.OnlineShop.Messages.Workflows;
using Msmq.OnlineShop.Common.Extensions;

namespace Msmq.OnlineShop.PackagingHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "PACKAGING";
            Console.WriteLine();
            const string queueAddress = @".\private$\packaging-queue";

            using (var queue = new MessageQueue(queueAddress))
            {
                queue.MulticastAddress = "234.1.1.1:8001";
                while (true)
                {
                    Console.WriteLine("Listening for events => PlaceOrderEvent...");
                    Console.WriteLine();

                    var message = queue.Receive();
                    var body = message.BodyStream.ReadFromJson(message.Label);
                    if (body.GetType() == typeof(OrderPlacedEvent))
                    {
                        var @event = body as OrderPlacedEvent;
                        Console.WriteLine("\tReceived: {0}, at {1}", @event.Product, DateTime.Now);

                        var workflow = new PackagingWorkflow(@event.Product);
                        workflow.Run();

                        Console.WriteLine("\tProcessed...");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
