using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Domain.Interfaces;
using BibliotecaServer.Infra.Context;

namespace BibliotecaServer.Infra.Repositories;
public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    private readonly BibliotecaDbContext _context;

    public UsuarioRepository(BibliotecaDbContext context) : base(context)
    {
        _context = context;
    }

}
