using Microsoft.EntityFrameworkCore;
using OrdersApi.Models;

public class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }
}