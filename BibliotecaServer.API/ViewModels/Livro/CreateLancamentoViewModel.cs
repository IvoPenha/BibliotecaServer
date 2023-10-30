using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaServer.API.ViewModels.Livro;
public class CreateLivroViewModel
{
    [Required(ErrorMessage = "Titulo é obrigatório")]
    [MinLength(3, ErrorMessage = "Titulo deve conter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Titulo deve conter no máximo 100 caracteres")]
    public string Titulo { get; set; } = null!;

    [Required(ErrorMessage = "Autor é obrigatório")]
    [MinLength(3, ErrorMessage = "Autor deve conter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Autor deve conter no máximo 100 caracteres")]
    public string Autor { get; set; } = null!;

    [Required(ErrorMessage = "Ano é obrigatório")]
    [Range(1400, int.MaxValue, ErrorMessage = "Ano deve ser maior que 0")]
    [DefaultValue(2000)]
    public int Ano { get; set; }


}

