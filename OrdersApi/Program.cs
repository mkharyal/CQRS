using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OrdersApi.Models;

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

app.MapPost("/api/orders", async (AppDbContext context, Order order) =>
{
    await context.Orders.AddAsync(order);
    await context.SaveChangesAsync();

    return Results.Created($"/api/orders/{order.Id}", order);
});

app.MapGet("/api/orders/{id}", async (AppDbContext context, int id) =>
    await context.Orders.FindAsync(id) is Order order
        ? Results.Ok(order)
        : Results.NotFound());

app.Run();