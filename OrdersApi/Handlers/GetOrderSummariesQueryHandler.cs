using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetOrderSummariesQueryHandler(ReadDbContext dbContext) : IRequestHandler<GetOrderSummariesQuery, IEnumerable<OrderSummaryDto>>
{
    public async Task<IEnumerable<OrderSummaryDto>> Handle(GetOrderSummariesQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .Select(o => new OrderSummaryDto(
                o.Id,
                $"{o.FirstName} {o.LastName}",
                o.Status,
                o.TotalCost))
            .ToListAsync(cancellationToken);
    }
}