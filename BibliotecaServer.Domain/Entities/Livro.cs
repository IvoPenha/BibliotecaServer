using BibliotecaServer.Domain.Validators;

namespace BibliotecaServer.Domain.Entities;

public class Livro : BaseEntity
{
    public Livro()
    {
        _errors = new List<string>();
    }
    
    public string Titulo { get; set; } = null!;
    public string Autor { get; set; } = null!;
    public int Ano { get; set; } = 0;

    public bool Disponibilidade { get; set; } = true;

    public bool Validate()
        => base.Validate(new LivroValidator(), this);

    public Livro(string titulo, string autor, int ano, bool disponibilidade)
    {
        Titulo = titulo;
        Autor = autor;
        Ano = ano;
        Disponibilidade = disponibilidade;

        _errors = new List<string>();

        Validate();
    }

}

