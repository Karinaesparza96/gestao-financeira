namespace Business.Interfaces
{
    public interface ILimiteOrcamentoTransacaoService
    {
        Task<bool> ValidarLimiteExcedido(string usuarioId, DateOnly periodo);

        bool TemRecursoDisponivel(decimal limite);
    }
}
