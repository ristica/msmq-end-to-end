# msmq-end-to-end


MSMQ - first steps with this messaging technology

This is small project demonstrationg the request-response pattern in msmq and then publishing events and subscribing to them by 
listener (multicast pattern).


To run the solution you need to restore nuget packages und run following projects
- Msmq.OnlineShop.Client
- Msmq.OnlineShop.InventoryHandler
- Msmq.OnlineShop.PackagingHandler
- Msmq.OnlineShop.SalesHandler
- Msmq.OnlineShop.ShippingHandler


Request-Response:
Client checks if there is enough products in stock (hardcoded stock: 8), by placing a message in the queue and waiting for the response.
Response comes back from the InventoryManager.

Multicast:
Client places an order. Order will be handled by SalesHandler which then fires an event (OrderPlacedevent) and pushes message to the queue. At this endpoint, there are 2 Handlers (PAckagingHAndler and ShippingHandler) listening...

