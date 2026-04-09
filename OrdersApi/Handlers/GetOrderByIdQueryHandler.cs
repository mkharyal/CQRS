using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Queries;

public class GetOrderByIdQueryHandler(ReadDbContext context) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        return order is null ? null : new OrderDto(order.Id, order.FirstName, order.LastName, order.Status, order.CreatedAt, order.TotalCost);
    }
}