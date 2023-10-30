using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Domain.Interfaces;
using BibliotecaServer.Infra.Context;

namespace BibliotecaServer.Infra.Repositories;
public class LivroRepository : BaseRepository<Livro>, ILivroRepository
{
    private readonly BibliotecaDbContext _context;

    public LivroRepository(BibliotecaDbContext context) : base(context)
    {
        _context = context;
    }

}
