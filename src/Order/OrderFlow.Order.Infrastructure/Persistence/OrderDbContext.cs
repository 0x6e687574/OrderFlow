using Microsoft.EntityFrameworkCore;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Infrastructure.Persistence;

using Order = Domain.Entities.Order;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<OrderSagaState> OrderSagaStates { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<InboxMessage> InboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}