namespace BibliotecaServer.Application.DTO;
public class LivroDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Autor { get; set; } = null!;
    public int Ano { get; set; } = 0;
    public bool Disponibilidade { get; set; } = true;
}

