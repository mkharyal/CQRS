using MediatR;
using OrdersApi.Models;

public class OrderCreatedProjectionHandler(ReadDbContext readDbContext) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent @event, CancellationToken cancellationToken)
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

        await readDbContext.Orders.AddAsync(order, cancellationToken);
        await readDbContext.SaveChangesAsync(cancellationToken);
    }
}