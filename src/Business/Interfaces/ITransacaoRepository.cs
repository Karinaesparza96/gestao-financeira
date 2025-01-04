using Business.Entities;
using Business.FiltrosBusca;

namespace Business.Interfaces
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtro, string usuarioIdentityId);

        decimal ObterSaldoTotal(string usuarioIdentityId);

        Task<bool> VerificarLimiteExcedido(string usuarioId, DateOnly periodo);
    }
}
