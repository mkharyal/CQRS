using OrdersApi.Models;
using OrdersApi.Queries;

public class GetOrderByIdQueryHandler
{
    public static async Task<Order?> Handle(GetOrderByIdQuery query, AppDbContext context)
    {
        return await context.Orders.FindAsync(query.OrderId);
    }
}