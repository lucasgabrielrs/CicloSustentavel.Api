using CicloSustentavel.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CicloSustentavel.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<EmpresaModel> Empresas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração de ProductModel
        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.Property(p => p.Category)
                .HasConversion(new EnumToStringConverter<Domain.Enums.Category>());

            entity.Property(p => p.UnitOfMeasurement)
                .HasConversion(new EnumToStringConverter<Domain.Enums.UnitOfMeasurement>());

            entity.Property(p => p.PackagingType)
                .HasConversion(new EnumToStringConverter<Domain.Enums.PackagingType>());

            entity.HasOne(p => p.Empresa)
                .WithMany()
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração de EmpresaModel
        modelBuilder.Entity<EmpresaModel>(entity =>
        {
            entity.HasIndex(e => e.Cnpj).IsUnique();
        });

        // Configuração de relacionamento N:N entre User e Empresa
        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.Empresas)
            .WithMany(e => e.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserEmpresas",
                j => j.HasOne<EmpresaModel>()
                      .WithMany()
                      .HasForeignKey("EmpresaId")
                      .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<UserModel>()
                      .WithMany()
                      .HasForeignKey("UserId")
                      .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("UserId", "EmpresaId");
                    j.ToTable("UserEmpresas");
                });

        base.OnModelCreating(modelBuilder);
    }
}