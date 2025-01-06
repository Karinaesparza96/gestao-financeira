using Business.Entities;
using Business.FiltrosBusca;

namespace Business.Interfaces
{
    public interface ILimiteOrcamentoRepository : IRepository<LimiteOrcamento>
    {
        Task<IEnumerable<LimiteOrcamento>> ObterTodos(FiltroLimiteOrcamento filtro, string usuarioIdentityId);
    }
}
