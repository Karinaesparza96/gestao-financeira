using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Notificacoes;
using Business.Services.Base;
using Business.Utils;

namespace Business.Services
{
    public class LimiteOrcamentoTransacaoService(ILimiteOrcamentoRepository limiteOrcamentoRepository,
                                                 IAppIdentityUser appIdentityUser,
                                                 INotificador notificador,
                                                 ITransacaoRepository transacaoRepository) : BaseService(appIdentityUser, notificador), ILimiteOrcamentoTransacaoService
    {
        private readonly IAppIdentityUser _appIdentityUser = appIdentityUser;

        public bool TemRecursoDisponivel(decimal limite)
        {
            var saldoTotal = transacaoRepository.ObterSaldoTotal(_appIdentityUser.GetUserId());

            return saldoTotal > limite;
        }


        public async Task ValidarLimitesExcedido(string usuarioId, DateOnly periodo)
        {
            var limites = await limiteOrcamentoRepository.ObterTodos(new FiltroLimiteOrcamento { Periodo = periodo }, usuarioId);

            foreach (var limite in limites)
            {
               ValidarLimite(limite);
            }
        }

        public void ValidarLimite(LimiteOrcamento limite)
        {
            var totalTransacoesSaida = transacaoRepository.ObterValorTotalDeSaidasNoPeriodo(UsuarioId, limite.Periodo, limite.CategoriaId);

            if (ExcedeuLimite(totalTransacoesSaida, limite))
            {
                return;
            }

            VerificarPorcentagemAviso(totalTransacoesSaida, limite);
        }

        private bool ExcedeuLimite(decimal totalSaida, LimiteOrcamento limite)
        {
            if (totalSaida > limite.Limite)
            {
                var percentual = PercentualHelper.CalcularPercentualExcedido(totalSaida, limite.Limite);
                Notificar($"O limite {limite?.Categoria?.Nome ?? "Geral"} ultrapassou {percentual}%", TipoNotificacao.Aviso);
                return true;
            }
            return false;
        }
        private void VerificarPorcentagemAviso(decimal totalSaida, LimiteOrcamento limite)
        {
            var porcentualAtingido = PercentualHelper.CalcularPorcentagem(totalSaida, limite.Limite);
            if (porcentualAtingido >= limite.PorcentagemAviso)
            {
                Notificar($"O limite {limite?.Categoria?.Nome ?? "Geral"} atingiu {porcentualAtingido}%.", TipoNotificacao.Aviso);
            }
        }


    }
}
