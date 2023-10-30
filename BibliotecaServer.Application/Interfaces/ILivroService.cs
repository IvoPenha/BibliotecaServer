using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Core.Structs;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.Application.Utilities;

namespace BibliotecaServer.Application.Interfaces;
public interface ILivroService
{
    Task<Optional<LivroDTO>> CreateAsync(LivroDTO livro);
    Task<Optional<LivroDTO>> UpdateAsync(int id, LivroDTO livro);
    Task RemoveAsync(int id);
    Task<IList<LivroDTO>> SearchAsync(LivroSearchParameters parametros);
    Task<Optional<LivroDTO>> GetAsync(int id);
    Task<IList<LivroDTO>> GetAllAsync();

}