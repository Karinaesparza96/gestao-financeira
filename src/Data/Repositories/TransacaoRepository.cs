using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TransacaoRepository(AppDbContext dbContext) : Repository<Transacao>(dbContext), ITransacaoRepository
    {
        public async Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtro, string usuarioId)
        {
            var query = DbSet.AsNoTracking().AsQueryable();

            if (filtro.TipoTransacao is not null)
            {
                query = query.Where(x => x.Tipo == filtro.TipoTransacao);
            }

            if (filtro.CategoriaId is not null)
            {
                query = query.Where(x => x.CategoriaId == filtro.CategoriaId);
            }

            if (filtro.Data is not null)
            {
                query = query.Where(x => x.Data == filtro.Data);
            }

            query = query.Where(x => x.Usuario.Id == usuarioId);

            return await query.ToListAsync();
        }

        public decimal ObterSaldoTotal(string usuarioId)
        {
           var result = DbSet.Where(x => x.UsuarioId == usuarioId).Sum(x => x.Valor);
           
            return result;
        }

        public async Task<bool> VerificarLimiteExcedido(string usuarioId, DateOnly periodo)
        {
            var todosLimites = await dbContext.Set<LimiteOrcamento>().Where(l => l.UsuarioId == usuarioId && l.Periodo.Month == periodo.Month && l.Periodo.Year == periodo.Year).ToListAsync();

            var transacoesSaida = DbSet.Where(t => t.UsuarioId == usuarioId 
                                    && t.Tipo == TipoTransacao.Saida 
                                    && t.Data.Year == periodo.Year 
                                    && t.Data.Month == periodo.Month)
                                .GroupBy(x => x.CategoriaId).Select(g => new
                                    {
                                       CategoriaId = g.Key,
                                       TotalSaida = g.Sum(t => t.Valor)
                                    });
            // limite por categoria
            foreach (var limite in todosLimites.Where(x => !x.LimiteGeral))
            {
                var transacaoCategoria = transacoesSaida.FirstOrDefault(x => x.CategoriaId == limite.CategoriaId);
                if (transacaoCategoria != null && transacaoCategoria.TotalSaida > limite.Limite)
                {
                    return true;
                }
            }
            // limite geral
            var limiteGeral = todosLimites.FirstOrDefault(x => x.LimiteGeral);

            if (limiteGeral != null)
            {
                var totalSaida = Math.Abs(transacoesSaida.Sum(x => x.TotalSaida));

                if (totalSaida > limiteGeral.Limite) { return true; }

                var somaLimitesCategoria = todosLimites.Where(x => !x.LimiteGeral).Sum(x => x.Limite);

                if (somaLimitesCategoria > limiteGeral.Limite)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
