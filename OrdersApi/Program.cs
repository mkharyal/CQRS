using Commands.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrdersApi.Handlers;
using OrdersApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();

builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, OrderDto>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto?>, GetOrderByIdQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.MapPost("/api/orders", async (ICommandHandler<CreateOrderCommand, OrderDto> createOrderCommandHandler, CreateOrderCommand orderCommand) =>
{
    try
    {
        var createdOrder = await createOrderCommandHandler.HandleAsync(orderCommand);
    
        return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(ex.Errors);
    }
});

app.MapGet("/api/orders/{id}", async (IQueryHandler<GetOrderByIdQuery, OrderDto?> getOrderByIdQueryHandler, int id) =>
{
    return await getOrderByIdQueryHandler.HandleAsync(new GetOrderByIdQuery(id)) is OrderDto o ?
                    Results.Ok(o) :
                    Results.NotFound();
});

app.Run();