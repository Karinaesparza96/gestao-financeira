using Business.Entities;

namespace Business.Interfaces
{
    public interface ICategoriaService : IEntityService<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterTodos();
        Task<Categoria?> ObterPorNomeEUsuario(string nome, string usuarioId);
    }
}
