using OrdersApi.Models;

public class OrderCreatedProjectionHandler(ReadDbContext readDbContext) : IEventHandler<OrderCreatedEvent>
{
    public async Task HandleAsync(OrderCreatedEvent @event)
    {
        var order = new Order
        {
            Id = @event.OrderId,
            FirstName = @event.FirstName,
            LastName = @event.LastName,
            TotalCost = @event.TotalCost,
            Status = "Created",
            CreatedAt = DateTime.UtcNow
        };

        await readDbContext.Orders.AddAsync(order);
        await readDbContext.SaveChangesAsync();
    }
}