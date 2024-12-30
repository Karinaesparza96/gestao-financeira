using Business.Entities;

namespace Business.Interfaces
{
    public interface IEntityService<TEntity> where TEntity : Entity 
    {
        Task<TEntity?> ObterPorId(int id);
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Exluir(int id);
    }
}
