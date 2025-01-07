using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Notificacoes;
using Business.Services.Base;

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
        public async Task ValidarLimiteExcedido(string usuarioId, DateOnly periodo)
        {
            var saldoTotalPorCategoria = await transacaoRepository.ObterSaldoTotalCategoriaPorPeriodo(usuarioId, periodo);
            var limites = await limiteOrcamentoRepository.ObterTodos(new FiltroLimiteOrcamento { Periodo = periodo }, usuarioId);

            var limiteGeral = limites.FirstOrDefault(l => l.LimiteGeral);

            if (limiteGeral != null)
            {
                var saldoTotalGeral = saldoTotalPorCategoria.Sum(x => x.Value);
                var porcentagemAtingido = CalcularPorcentagem(saldoTotalGeral, limiteGeral.Limite);

                if (saldoTotalGeral > limiteGeral.Limite)
                {
                    Notificar("O limite geral definido foi excedido.", TipoNotificacao.Aviso);
                    return;
                }

                if ((double)porcentagemAtingido >= limiteGeral.PorcentagemAviso)
                {
                    Notificar($"O limite geral definido atingiu {porcentagemAtingido}%.");
                    return;
                }
            }

            foreach (var limiteCategoria in limites.Where(x => !x.LimiteGeral))
            {
                var totalsaidaCategoria = Math.Abs(saldoTotalPorCategoria.FirstOrDefault(x => x.Key == limiteCategoria.CategoriaId).Value);
                var porcentagemAtingido = CalcularPorcentagem(totalsaidaCategoria, limiteCategoria.Limite);

                if (totalsaidaCategoria > limiteCategoria.Limite)
                {   
                    Notificar($"O limite definido para categoria {limiteCategoria?.Categoria?.Nome} foi excedido.", TipoNotificacao.Aviso);
                    return;
                }

                if ((double)porcentagemAtingido >= limiteCategoria.PorcentagemAviso)
                {
                    Notificar($"O limite definido para categoria {limiteCategoria?.Categoria?.Nome} atingiu {porcentagemAtingido}%.", TipoNotificacao.Aviso);
                    return;
                } 

            }
        }

        private decimal CalcularPorcentagem(decimal totalSaida, decimal limite)
        {
            return (totalSaida / limite) * 100;
        }
    }
}
