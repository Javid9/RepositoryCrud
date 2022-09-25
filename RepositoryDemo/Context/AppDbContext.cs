using Microsoft.EntityFrameworkCore;
using RepositoryDemo.Entity;

namespace RepositoryDemo.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    public DbSet<User> Users { get; set; }


    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    // {
    //     foreach (var entry in ChangeTracker.Entries<BaseEntity>())
    //     {
    //         switch (entry.State)
    //         {
    //             case EntityState.Added:
    //                 entry.Entity.Created = _dateTime.NowUtc;
    //                 entry.Entity.CreatedBy = _authenticatedUser.UserId;
    //                 break;
    //             case EntityState.Modified:
    //                 entry.Entity.LastModified = _dateTime.NowUtc;
    //                 entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
    //                 break;
    //         }
    //     }
    //     return base.SaveChangesAsync(cancellationToken);
    // }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //All Decimals will have 18,6 Range
        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            property.SetColumnType("decimal(18,6)");

        base.OnModelCreating(builder);
    }
}