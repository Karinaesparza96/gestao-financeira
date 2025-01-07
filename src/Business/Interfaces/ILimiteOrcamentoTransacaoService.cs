namespace Business.Interfaces
{
    public interface ILimiteOrcamentoTransacaoService
    {
        Task ValidarLimiteExcedido(string usuarioId, DateOnly periodo);

        bool TemRecursoDisponivel(decimal limite);
    }
}
