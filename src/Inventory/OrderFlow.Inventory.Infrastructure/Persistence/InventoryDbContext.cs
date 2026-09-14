using Microsoft.EntityFrameworkCore;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Infrastructure.Persistence;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<InboxMessage> InboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
    }
}