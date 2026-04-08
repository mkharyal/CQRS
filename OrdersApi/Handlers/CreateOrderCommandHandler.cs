using FluentValidation;
using OrdersApi.Models;

namespace OrdersApi.Handlers
{
    public class CreateOrderCommandHandler(AppDbContext context, IValidator<CreateOrderCommand> validator) : ICommandHandler<CreateOrderCommand, OrderDto>
    {
        public async Task<OrderDto> HandleAsync(CreateOrderCommand command)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var order = new Order
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Status = command.Status,
                TotalCost = command.TotalCost,
                CreatedAt = DateTime.UtcNow
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return new OrderDto(order.Id, order.FirstName, order.LastName, order.Status, order.CreatedAt, order.TotalCost);
        }
    }
}