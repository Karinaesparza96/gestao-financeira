using Business.Entities;
using Business.FiltrosBusca;
using Business.ValueObjects;

namespace Business.Interfaces
{
    public interface ITransacaoService
    {
        Task<Transacao?> ObterPorId(Guid id);
        Task Adicionar(Transacao entityDto);
        Task Atualizar(Transacao entityDto);
        Task Excluir(Guid id);
        Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtroDto);
        Task<ResumoFinanceiro> ObterResumoEntradasESaidas();
    }
}
