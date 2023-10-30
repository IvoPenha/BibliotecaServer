using BibliotecaServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaServer.Infra.Mapping;

internal class LivroMapping : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Titulo).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Autor).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Ano).IsRequired();
        builder.Property(x => x.Disponibilidade).HasDefaultValue(true);
    }
}