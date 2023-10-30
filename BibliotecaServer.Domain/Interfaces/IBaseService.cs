using FluentValidation;
using BibliotecaServer.Domain.Entities;

namespace BibliotecaServer.Domain.Interfaces;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    TEntity Add<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>;
    void Delete(int id);
    IList<TEntity> Get();
    TEntity GetById(int id);
    TEntity Update<TValidator>(TEntity entity) where TValidator : AbstractValidator<TEntity>;

}

