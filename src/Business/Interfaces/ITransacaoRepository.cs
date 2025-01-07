using Business.Entities;
using Business.FiltrosBusca;

namespace Business.Interfaces
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtro, string usuarioIdentityId);

        decimal ObterSaldoTotal(string usuarioIdentityId);

        Task<Dictionary<int, decimal>> ObterSaldoTotalCategoriaPorPeriodo(string usuarioId, DateOnly periodo);
    }
}
