using com.Entities;
using System.Data.Entity;

namespace com.database
{
    public class CContext : DbContext
    { 
        public CContext() : base("EstoreConnect") {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<EStoreConfiguration> EStoreConfigurations { get; set; }
    }
}
