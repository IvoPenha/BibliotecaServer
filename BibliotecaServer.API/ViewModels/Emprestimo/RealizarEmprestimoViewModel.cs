using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaServer.API.ViewModels.Livro;
public class RealizarEmprestimoViewModel
{
    [Required(ErrorMessage = "LivroId é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "LivroId deve ser maior que 0")]
    [DefaultValue(0)]
    public int LivroId { get; set; }

    [Required(ErrorMessage = "UsuarioId é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "UsuarioId deve ser maior que 0")]
    [DefaultValue(0)]
    public int UsuarioId { get; set; }

}

