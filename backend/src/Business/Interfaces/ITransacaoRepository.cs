using Business.Entities;
using Business.FiltrosBusca;
using Business.ValueObjects;

namespace Business.Interfaces
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        new Task<Transacao?> ObterPorId(Guid id);
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtro, string usuarioIdentityId);

        decimal ObterSaldoTotal(string usuarioIdentityId);

        decimal ObterValorTotalDeSaidasNoPeriodo(string usuarioId, DateOnly periodo, Guid? categoriaId = null);

        Task<ResumoFinanceiro> ObterResumoEntradasESaidas(string usuarioId);
    }
}
