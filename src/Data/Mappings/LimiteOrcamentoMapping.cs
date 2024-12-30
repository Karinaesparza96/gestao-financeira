using Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class LimiteOrcamentoMapping : IEntityTypeConfiguration<LimiteOrcamento>
    {
        public void Configure(EntityTypeBuilder<LimiteOrcamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Periodo)
                .IsRequired();

            builder.Property(x => x.Limite)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.UsuarioId)
                .IsRequired();

            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaId)
                .IsRequired(false);

            builder.ToTable("LimitesOrcamentos");
        }
    }
}
