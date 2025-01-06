using Business.Entities;
using Business.FiltrosBusca;
using Business.ValueObjets;

namespace Business.Interfaces
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtro, string usuarioIdentityId);

        decimal ObterSaldoTotal(string usuarioIdentityId);

        Task<IEnumerable<TotalSaidaCategoria>> ObterSaldoTotalCategoriaPorPeriodo(string usuarioId, DateOnly periodo);
    }
}
