MSMQ - how to implement a messaging technology

----------------------------------------------

This one demonstrates:
- request / response (pattern)
- publish / subscribe (events)
- msmq
- multicast (pattern)


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

Client places an order. Order will be handled by SalesHandler, who "fires" an event (OrderPlacedevent) and a message is going to be posted  to the queue. There are 2 subscribers (PackagingHandler and ShippingHandler) listening at this endpoint...
