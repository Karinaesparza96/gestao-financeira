using Business.Entities;
using Business.FiltrosBusca;

namespace Business.Interfaces
{
    public interface ILimiteOrcamentoService : IEntityService<LimiteOrcamento>
    {
        Task<IEnumerable<LimiteOrcamento>> ObterTodos(FiltroLimiteOrcamento filtro);
        public decimal ObterValorTotalDeSaidasNoPeriodo(string usuarioId, DateOnly periodo, Guid? categoriaId);
    }
}
