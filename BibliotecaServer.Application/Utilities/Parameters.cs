using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaServer.Application.Utilities;

public class LivroSearchParameters
{
    public string? Titulo { get; set; }
    public string? Autor { get; set; }
    public string? Ano { get; set; }
}
