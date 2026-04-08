using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class CreateOrderCommandHandler(AppDbContext context) : ICommandHandler<CreateOrderCommand, OrderDto>
    {
        public async Task<OrderDto> HandleAsync(CreateOrderCommand command)
        {
            var order = new Order
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Status = command.Status,
                TotalCost = command.Cost,
                CreatedAt = DateTime.UtcNow
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return new OrderDto(order.Id, order.FirstName, order.LastName, order.Status, order.CreatedAt, order.TotalCost);
        }
    }
}