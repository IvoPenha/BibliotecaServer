using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Core.Structs;
using BibliotecaServer.Application.DTO;
using BibliotecaServer.Application.Utilities;

namespace BibliotecaServer.Application.Interfaces;
public interface IEmprestimoService
{
    Task<Optional<EmprestimoDTO>> CreateAsync(int idLivro, int idUsuario);
    Task<Optional<LivroDTO>> Devolver (int idLivro);
    Task RemoveAsync(int id);
    Task<Optional<EmprestimoDTO>> GetAsync(int id);
    Task<Optional<IList<EmprestimoDTO>>> GetAllAsync();


}