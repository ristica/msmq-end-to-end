using System;
using System.Diagnostics;
using System.Messaging;
using Msmq.OnlineShop.Common.Extensions;
using Msmq.OnlineShop.Messages.Queries;

namespace Msmq.OnlineShop.ApplicationServices
{
    public class IsProductInStockService
    {
        private const string RequestQueue = @".\private$\product-in-stock-queue";

        public int GetCurrentStock(int productId)
        {
            // create new queue where the application service
            // is going to listen for the respnse => stock amount
            var responseAddress = Guid.NewGuid().ToString().Substring(0, 8);
            responseAddress = @".\private$\" + responseAddress;

            try
            {
                // create response queue for listening
                var responseQueue = MessageQueue.Create(responseAddress);

                // create request object
                var request = new ProductInStockRequest
                {
                    ProductId = productId
                };

                // create reuest queue and send message
                using (var requestQueue = new MessageQueue(RequestQueue))
                {
                    var message = new Message
                    {
                        BodyStream = request.ToJsonStream(),
                        Label = request.GetMessageType(),
                        ResponseQueue = responseQueue
                    };
                    requestQueue.Send(message);
                }

                // wait for the response
                var response = responseQueue.Receive();
                if (response == null) return 0;

                var responseBody = response.BodyStream.ReadFromJson<ProductInStockResponse>();
                return responseBody.Stock;
            }
            catch (Exception ex)
            {
                Debugger.Break();
                throw;
            }
            finally
            {
                if (MessageQueue.Exists(responseAddress))
                {
                    MessageQueue.Delete(responseAddress);
                }
            }
        }
    }
}
