using Business.Entities;
using Business.FiltrosBusca;

namespace Business.Interfaces
{
    public interface ITransacaoService : IEntityService<Transacao>
    {
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtroDto);
    }
}
