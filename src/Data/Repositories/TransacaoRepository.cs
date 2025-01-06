using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.ValueObjets;
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

        public async Task<IEnumerable<TotalSaidaCategoria>> ObterSaldoTotalCategoriaPorPeriodo(string usuarioId, DateOnly periodo)
        {
            return await DbSet.Where(t => t.UsuarioId == usuarioId
                                    && t.Data.Year == periodo.Year
                                    && t.Data.Month == periodo.Month)
                                .GroupBy(x => x.CategoriaId).Select(g => new TotalSaidaCategoria
                                {
                                    CategoriaId = g.Key,
                                    TotalSaida = g.Sum(t => t.Valor)
                                }).ToListAsync();
        }
    }
}
