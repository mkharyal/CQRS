using Microsoft.EntityFrameworkCore;

public class GetOrderSummariesQueryHandler(ReadDbContext dbContext) : IQueryHandler<GetOrderSummariesQuery, IEnumerable<OrderSummaryDto>>
{
    public async Task<IEnumerable<OrderSummaryDto>> HandleAsync(GetOrderSummariesQuery query)
    {
        return await dbContext.Orders
            .Select(o => new OrderSummaryDto(
                o.Id,
                $"{o.FirstName} {o.LastName}",
                o.Status,
                o.TotalCost))
            .ToListAsync();
    }
}