using BibliotecaServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaServer.Infra.Mapping;

internal class EmprestimoMapping : IEntityTypeConfiguration<Emprestimo>
{
    public void Configure(EntityTypeBuilder<Emprestimo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LivroId).IsRequired();
        builder.Property(x => x.UsuarioId).IsRequired();
        builder.Property(x => x.DataEmprestimo).IsRequired();
        builder.Property(x => x.DataDevolucao).IsRequired();
    }
}