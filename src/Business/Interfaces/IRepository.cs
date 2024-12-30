using Business.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<List<TEntity>> ObterTodos();

        Task<TEntity?> ObterPorId(int id);

        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);

        Task Atualizar(TEntity entity);

        Task Adicionar(TEntity entity);

        Task Excluir(TEntity entity);
    }
}
