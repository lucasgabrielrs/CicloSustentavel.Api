using CicloSustentavel.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CicloSustentavel.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<ProductModel> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Converte o Enum Category para String no banco
        modelBuilder.Entity<ProductModel>()
            .Property(p => p.Category)
            .HasConversion(new EnumToStringConverter<Domain.Enums.Category>());

        // Converte o Enum UnitOfMeasurement para String no banco
        modelBuilder.Entity<ProductModel>()
            .Property(p => p.UnitOfMeasurement)
            .HasConversion(new EnumToStringConverter<Domain.Enums.UnitOfMeasurement>());

        // Converte o Enum PackagingType para String no banco
        modelBuilder.Entity<ProductModel>()
            .Property(p => p.PackagingType)
            .HasConversion(new EnumToStringConverter<Domain.Enums.PackagingType>());

        base.OnModelCreating(modelBuilder);
    }
}