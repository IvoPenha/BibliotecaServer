using System.Threading.Tasks;
using System.Collections.Generic;
using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Core.Structs;
using BibliotecaServer.Application.DTO;

namespace BibliotecaServer.Application.Interfaces;
public interface IUsuarioService
{
    Task<Optional<UsuarioDTO>> CreateAsync(UsuarioDTO usuarioDTO);
    Task<Optional<UsuarioDTO>> UpdateAsync(int id, UsuarioDTO usuarioDTO);
    Task RemoveAsync(int id);
    Task<Optional<UsuarioDTO>> GetAsync(int id);
    Task<Optional<IList<UsuarioDTO>>> GetAllAsync();
}