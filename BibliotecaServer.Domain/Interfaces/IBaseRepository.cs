using System.Linq.Expressions;
using BibliotecaServer.Domain.Entities;

namespace BibliotecaServer.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntity obj);
    Task<TEntity> UpdateAsync(TEntity obj);
    Task RemoveAsync(long id);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(long id);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool asNoTracking = true);
    Task<IList<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> expression, bool asNoTracking = true);
}