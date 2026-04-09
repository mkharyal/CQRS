using MediatR;

public record GetOrderSummariesQuery() : IRequest<IEnumerable<OrderSummaryDto>>;