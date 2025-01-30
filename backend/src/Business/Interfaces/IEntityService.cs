namespace Business.Interfaces
{
    public interface IEntityService<T> where T : class
    {
        Task<T?> ObterPorId(Guid id);
        Task Adicionar(T entityDto);
        Task Atualizar(T entityDto);
        Task Excluir(Guid id);
    }
}
