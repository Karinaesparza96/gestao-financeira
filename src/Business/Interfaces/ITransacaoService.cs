using Business.Entities;
using Business.FiltrosBusca;

namespace Business.Interfaces
{
    public interface ITransacaoService : IEntityService<Transacao>
    {
        Task<ResultadoOperacao<IEnumerable<Transacao>>> ObterTodos(FiltroTransacao filtroDto);
    }
}
