using Ecommerce.Domain;
using Ecommerce.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Infraestructure.Persistence;

public class EcommerceDbContext : IdentityDbContext<User>
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) :  base(options)
    {}

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userName = "system";
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifyBy = userName;
                    break;
                default:
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        // lenguaje fluent api agrega caracteristicas o reglas adicionales a ciertas entidades()
        // relacion una categoria a muchos productos
        builder.Entity<Category>()
        .HasMany(p => p.Products)
        .WithOne(r => r.Category)
        // establece la llave fornaea en la tabla producto
        .HasForeignKey(r => r.CategoryId)
        .IsRequired()
        // RESTRICT si eliminan una categoria no se elimina el producto relacionados a esa categoria
        .OnDelete(DeleteBehavior.Restrict);

        // relación un producto puede tener muchas review y un review solo asociado a un producto
        builder.Entity<Product>()
        .HasMany(p => p.Reviews)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .IsRequired()
        // CASCADE si se elimina un producto se elimina todo los revies relacionados
        .OnDelete(DeleteBehavior.Cascade);

        // relación un producto puede tener muchas IMAGENES y un imagen solo asociado a un producto
        builder.Entity<Product>()
        .HasMany(p => p.Images)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);


        // relación de un shopping cart tiene muchos shoppingcartitems y un shoppingcartitems solo asociado a un shopping cart
        builder.Entity<ShoppingCart>()
        .HasMany(p => p.ShoppingCartItems)
        .WithOne(r => r.ShoppingCart)
        .HasForeignKey(r => r.ShoppingCartId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);

        


        builder.Entity<User>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<User>().Property(x => x.NormalizedUserName).HasMaxLength(90);
        builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(36);
        builder.Entity<IdentityRole>().Property(x => x.NormalizedName).HasMaxLength(90);
    }


    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Image>? Images { get; set; }
    public DbSet<Address>? Addresses { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderItem>? OrderItems { get; set; }
    public DbSet<Review>? Reviews { get; set; }
    public DbSet<ShoppingCart>? ShoppingCarts { get; set; }
    public DbSet<ShoppingCartItem>? ShoppingCartItems { get; set; }
    public DbSet<Country>? Countries { get; set; }
    public DbSet<OrderAddress>? OrderAddresses { get; set; }
}
