using Business.Entities;

namespace Business.Interfaces
{
    public interface IEntityService<T> where T : class
    {
        Task<ResultadoOperacao<T>> ObterPorId(int id);
        Task<ResultadoOperacao> Adicionar(T entityDto);
        Task<ResultadoOperacao> Atualizar(T entityDto);
        Task<ResultadoOperacao> Exluir(int id);
    }
}
