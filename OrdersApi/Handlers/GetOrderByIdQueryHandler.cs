using OrdersApi.Models;
using OrdersApi.Queries;

public class GetOrderByIdQueryHandler(AppDbContext context) : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await context.Orders.FindAsync(query.OrderId);
        return order is null ? null : new OrderDto(order.Id, order.FirstName, order.LastName, order.Status, order.CreatedAt, order.TotalCost);
    }
}