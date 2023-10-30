using BibliotecaServer.Domain.Entities;
using BibliotecaServer.Domain.Interfaces;
using BibliotecaServer.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaServer.Infra.Repositories;
public class EmprestimoRepository : BaseRepository<Emprestimo>, IEmprestimoRepository
{
    private readonly BibliotecaDbContext _context;

    public EmprestimoRepository(BibliotecaDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Emprestimo?> getEmprestimoByLivroID(int id)
    {
        var emprestimo = await _context.Emprestimos.Where(x => x.LivroId == id).FirstOrDefaultAsync<Emprestimo>();

        if (emprestimo == null)
        {
               return null;
        }

        return emprestimo;

    }

}
