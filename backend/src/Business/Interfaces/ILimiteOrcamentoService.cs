using Business.Entities;
using Business.FiltrosBusca;

namespace Business.Interfaces
{
    public interface ILimiteOrcamentoService : IEntityService<LimiteOrcamento>
    {
        Task<IEnumerable<LimiteOrcamento>> ObterTodos(FiltroLimiteOrcamento filtro);
    }
}
