using Microsoft.EntityFrameworkCore;
using OrdersApi.Models;

public class ReadDbContext : DbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }
}