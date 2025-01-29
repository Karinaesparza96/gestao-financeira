using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.ValueObjects;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

        public decimal ObterValorTotalDeSaidasNoPeriodo(string usuarioId, DateOnly periodo, int? categoriaId)
        {
            var query = DbSet.Where(t => t.UsuarioId == usuarioId
                                         && t.Data.Year == periodo.Year
                                         && t.Data.Month == periodo.Month
                                         && t.Tipo == TipoTransacao.Saida);
            if (categoriaId.HasValue)
            {
                query = query.Where(t => t.CategoriaId == categoriaId);
            }
                
            var result = query.Sum(t => t.Valor);
            return result;
        }


        public async Task<ResumoFinanceiro> ObterResumoEntradasESaidas(string usuarioId)
        {   
            var query = await DbSet
                .Where(x => x.UsuarioId == usuarioId)
                .GroupBy(t => t.Tipo).ToDictionaryAsync(
                    g => g.Key,
                    g => g.Sum(t => t.Valor)
                );

            var resumo = new ResumoFinanceiro
            {
                TotalReceita = query.FirstOrDefault(x => x.Key == TipoTransacao.Entrada).Value,
                TotalDespesa = query.FirstOrDefault(x => x.Key == TipoTransacao.Saida).Value
            };

            return resumo;
           
        }
    }
}
