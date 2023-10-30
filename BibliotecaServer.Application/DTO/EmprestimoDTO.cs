namespace BibliotecaServer.Application.DTO;
public class EmprestimoDTO
{
    public int Id { get; set; }
    public int LivroId { get; set; }
    public int UsuarioId { get; set; }
    public DateOnly DataEmprestimo { get; set;}
    public DateOnly DataDevolucao { get; set; }
}

