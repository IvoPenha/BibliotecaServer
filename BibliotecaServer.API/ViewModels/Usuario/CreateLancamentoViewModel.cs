using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaServer.API.ViewModels.Usuario;
public class CreateUsuarioViewModel
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve conter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Nome deve conter no máximo 100 caracteres")]
    public string Nome { get; set; } = null!;

    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    [MinLength(3, ErrorMessage = "Email deve conter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Email deve conter no máximo 100 caracteres")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Telefone é obrigatório")]
    [MinLength(11, ErrorMessage = "Telefone deve conter no mínimo 11 caracteres")]
    [MaxLength(18, ErrorMessage = "Telefone deve conter no máximo 18 caracteres")]
    public string Telefone { get; set; } = null!;

}

