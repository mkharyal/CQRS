using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OrdersApi.Handlers;
using OrdersApi.Models;
using OrdersApi.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.MapPost("/api/orders", async (AppDbContext context, CreateOrderCommand orderCommand) =>
{
    var createdOrder = await CreateOrderCommandHandler.Handle(orderCommand, context);

    return Results.Created($"/api/orders/{createdOrder.Id}", createdOrder);
});

app.MapGet("/api/orders/{id}", async (AppDbContext context, int id) =>
{
    return await GetOrderByIdQueryHandler.Handle(new GetOrderByIdQuery(id), context) is Order o ?
                    Results.Ok(o) :
                    Results.NotFound();
});

app.Run();