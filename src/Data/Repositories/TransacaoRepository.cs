using Business.Dtos;
using Business.Entities;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TransacaoRepository(AppDbContext dbContext) : Repository<Transacao>(dbContext), ITransacaoRepository
    {
        public async Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacaoDto filtro, string usuarioIdentityId)
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

            query = query.Where(x => x.Usuario.Id == usuarioIdentityId);

            return await query.ToListAsync();
        }
    }
}
