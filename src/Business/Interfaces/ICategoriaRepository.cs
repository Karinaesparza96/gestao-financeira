using Business.Entities;

namespace Business.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria?> ObterPorId(int id, string usuarioIdentityId);

        Task<IEnumerable<Categoria>> ObterCategoriasPadrao();
    }
}
