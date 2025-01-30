using Business.Entities;

namespace Business.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria?> ObterTransacoes(Guid id);

    }
}
