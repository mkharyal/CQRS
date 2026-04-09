using Commands.Validators;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Handlers;
using OrdersApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<WriteDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WriteDataDbConnection")));
builder.Services.AddDbContext<ReadDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ReadDataDbConnection")));

builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.MapPost("/api/orders", async (IMediator mediator, CreateOrderCommand orderCommand) =>
{
    try
    {
        var createdOrder = await mediator.Send(orderCommand);
    
        return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(ex.Errors);
    }
});

app.MapGet("/api/orders/{id}", async (IMediator mediator, int id) =>
{
    return await mediator.Send(new GetOrderByIdQuery(id)) is OrderDto o ?
                    Results.Ok(o) :
                    Results.NotFound();
});

app.MapGet("/api/orders", async (IMediator mediator) =>     
{
    return await mediator.Send(new GetOrderSummariesQuery()) is IEnumerable<OrderSummaryDto> orderSummaries ?
                    Results.Ok(orderSummaries) :
                    Results.NotFound();
});

app.Run();