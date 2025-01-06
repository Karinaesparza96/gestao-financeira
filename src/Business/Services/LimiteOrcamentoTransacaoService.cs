using Business.FiltrosBusca;
using Business.Interfaces;

namespace Business.Services
{
    public class LimiteOrcamentoTransacaoService(ILimiteOrcamentoRepository limiteOrcamentoRepository,
                                                 IAppIdentityUser appIdentityUser,
                                                 ITransacaoRepository transacaoRepository) : ILimiteOrcamentoTransacaoService
    {
        public bool TemRecursoDisponivel(decimal limite)
        {
            var saldoTotal = transacaoRepository.ObterSaldoTotal(appIdentityUser.GetUserId());

            return saldoTotal > limite;
        }
        public async Task<bool> ValidarLimiteExcedido(string usuarioId, DateOnly periodo)
        {
            var totalSaidasCategoria = await transacaoRepository.ObterSaldoTotalCategoriaPorPeriodo(usuarioId, periodo);
            var limites = await limiteOrcamentoRepository.ObterTodos(new FiltroLimiteOrcamento { Periodo = periodo }, usuarioId);

            // verificar se limite geral for excedido
            var limiteGeral = limites.FirstOrDefault(l => l.LimiteGeral);

            if (limiteGeral != null)
            {
                var totalSaida = Math.Abs(totalSaidasCategoria.Sum(t => t.TotalSaida));
                var porcentagemAtingido = CalcularPorcentagem(totalSaida, limiteGeral.Limite);

                if (totalSaida > limiteGeral.Limite)
                {
                    // Notificar
                    return true;
                }

                if ((double)porcentagemAtingido >= limiteGeral.PorcentagemAviso)
                {
                    // Notificar
                    return true;
                }
            }

            //verificar se limite por categoria for excedido
            foreach (var limiteCategoria in limites.Where(x => !x.LimiteGeral))
            {
                var totalsaidaCategoria = Math.Abs(totalSaidasCategoria.FirstOrDefault(x => x.CategoriaId == limiteCategoria.CategoriaId)?.TotalSaida ?? 0);
                var porcentagemAtingido = CalcularPorcentagem(totalsaidaCategoria, limiteCategoria.Limite);

                if (totalsaidaCategoria > limiteCategoria.Limite) { return true; } // notificar

                if ((double)porcentagemAtingido >= limiteCategoria.PorcentagemAviso) { return true; } // notificar

            }
            return false;
        }

        private decimal CalcularPorcentagem(decimal totalSaida, decimal limite)
        {
            return (totalSaida / limite) * 100;
        }
    }
}
