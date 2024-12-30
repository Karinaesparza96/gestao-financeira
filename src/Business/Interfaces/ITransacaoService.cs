using Business.Dtos;
using Business.Entities;

namespace Business.Interfaces
{
    public interface ITransacaoService : IEntityService<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacaoDto filtroDto);
    }
}
