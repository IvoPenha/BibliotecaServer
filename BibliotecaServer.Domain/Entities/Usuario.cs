using BibliotecaServer.Domain.Validators;

namespace BibliotecaServer.Domain.Entities;

public class Usuario : BaseEntity
{
    public Usuario()
    {
        _errors = new List<string>();
    }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefone { get; set; } = null!;
    
    
    public bool Validate()
        => base.Validate(new UsuarioValidator(), this);

    public Usuario(string nome, string email, string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;

        _errors = new List<string>();

        Validate();
    }


}