using Business.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<List<TEntity>> ObterTodos();

        Task<TEntity?> ObterPorId(Guid id);

        Task<IEnumerable<TEntity>> Buscar<TOrderKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TOrderKey>>? orderBy = null);

        Task Atualizar(TEntity entity);

        Task Adicionar(TEntity entity);

        Task Excluir(TEntity entity);
    }
}
