using Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Tipo)
                .IsRequired();

            builder.Property(t => t.Data)
                .IsRequired();

            builder.Property(t => t.Descricao)
                .HasColumnType("varchar(200)");

            builder.HasOne(t => t.Categoria)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.CategoriaId)
                .IsRequired();

            builder.HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .IsRequired();

            builder.ToTable("Transacoes");

        }
    }
}
