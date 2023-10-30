using BibliotecaServer.Domain.Validators;

namespace BibliotecaServer.Domain.Entities;
public class Emprestimo : BaseEntity
{
    public Emprestimo()
    {
        _errors = new List<string>();
    }
    public int LivroId { get; set; }
    public int UsuarioId { get; set; }
    public DateOnly DataEmprestimo { get; set;}
    public DateOnly DataDevolucao { get; set; }
    public bool Validate()
        => base.Validate(new EmprestimoValidator(), this);

    public Emprestimo(int livroId, int usuarioId, DateOnly dataEmprestimo, DateOnly dataDevolucao)
    {
        LivroId = livroId;
        UsuarioId = usuarioId;
        DataEmprestimo = dataEmprestimo;
        DataDevolucao = dataDevolucao;

        _errors = new List<string>();

        Validate();
    }

}

