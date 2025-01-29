namespace Business.Interfaces
{
    public interface ILimiteOrcamentoTransacaoService
    {
        Task ValidarLimitesExcedido(string usuarioId, DateOnly periodo);
        bool TemRecursoDisponivel(decimal limite);
    }
}
