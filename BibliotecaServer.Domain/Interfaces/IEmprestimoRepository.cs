using BibliotecaServer.Domain.Entities;

namespace BibliotecaServer.Domain.Interfaces;


public interface IEmprestimoRepository : IBaseRepository<Emprestimo>
{
    Task<Emprestimo?> getEmprestimoByLivroID(int id);
}
