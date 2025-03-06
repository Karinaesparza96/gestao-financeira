using Business.Entities;

namespace Business.Interfaces
{
    public interface ICategoriaService : IEntityService<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterTodos();
    }
}
