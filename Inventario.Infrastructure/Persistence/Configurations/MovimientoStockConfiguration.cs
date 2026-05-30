using Inventario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventario.Infrastructure.Persistence.Configurations;

public class MovimientoStockConfiguration : IEntityTypeConfiguration<MovimientoStock>
{
    public void Configure(EntityTypeBuilder<MovimientoStock> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Comentario).HasMaxLength(500);
        
        builder.ToTable(t => t.HasCheckConstraint("CK_MovimientoStock_Cantidad", "Cantidad > 0"));
    }
}
