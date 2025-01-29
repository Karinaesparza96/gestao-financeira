namespace Business.Interfaces
{
    public interface IEntityService<T> where T : class
    {
        Task<T?> ObterPorId(int id);
        Task Adicionar(T entityDto);
        Task Atualizar(T entityDto);
        Task Exluir(int id);
    }
}
