using MediatR;

namespace OrdersApi.Queries
{
    public record GetOrderByIdQuery(int OrderId) : IRequest<OrderDto?>;
}