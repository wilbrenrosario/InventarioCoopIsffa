using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Persistence.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(c => c.Nombre).IsUnique();

        builder.HasMany(c => c.Productos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed
        builder.HasData(
            new Categoria { Id = 1, Nombre = "Electrónica" },
            new Categoria { Id = 2, Nombre = "Hogar" },
            new Categoria { Id = 3, Nombre = "Ferretería" }
        );
    }
}
