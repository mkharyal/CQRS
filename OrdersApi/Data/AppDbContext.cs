using Microsoft.EntityFrameworkCore;
using OrdersApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }
}