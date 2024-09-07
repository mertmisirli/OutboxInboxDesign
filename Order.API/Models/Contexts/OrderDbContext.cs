using Microsoft.EntityFrameworkCore;
using Order.API.Models.Entities;

namespace Order.API.Models.Contexts
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order.API.Models.Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }

        public DbSet<OrderOutbox> OrderOutboxes { get;set; }
    }
}
