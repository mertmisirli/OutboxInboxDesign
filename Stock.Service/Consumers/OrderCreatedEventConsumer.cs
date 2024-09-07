using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Stock.Service.Models.Contexts;
using Stock.Service.Models.Entities;
using System.Text.Json;

namespace Stock.Service.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        StockDbContext _stockDbContext;

        public OrderCreatedEventConsumer(StockDbContext stockDbContext)
        {
            _stockDbContext = stockDbContext;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var result = await _stockDbContext.OrderInboxes.AnyAsync(i => i.Id == context.Message.OrderId);
            if (!result)
            {

                await _stockDbContext.OrderInboxes.AddAsync(new()
                {
                    Processed = false,
                    Payload = JsonSerializer.Serialize(context.Message),
                    Id = context.Message.OrderId
                });

                await _stockDbContext.SaveChangesAsync();
            }


            List<OrderInbox> orderInboxes = await _stockDbContext.OrderInboxes
                .Where(i => i.Processed == false)
                .ToListAsync();
            foreach (var orderInbox in orderInboxes)
            {
                OrderCreatedEvent orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderInbox.Payload);
                Console.WriteLine($"{orderCreatedEvent.OrderId} order id değerine karşılık olan siparişin stok işlemleri başarıyla tamamlanmıştır.");
                orderInbox.Processed = true;
                await _stockDbContext.SaveChangesAsync();
            }
        }
    }
}
