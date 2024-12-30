using Business.Dtos;
using Business.Entities;

namespace Business.Interfaces
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacaoDto filtro, string usuarioIdentityId);
    }
}
