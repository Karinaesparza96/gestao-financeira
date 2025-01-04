using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class LimiteOrcamentoRepository(AppDbContext dbContext) : Repository<LimiteOrcamento>(dbContext), ILimiteOrcamentoRepository
    {
        public async Task<IEnumerable<LimiteOrcamento>> ObterTodos(FiltroLimiteOrcamento filtro, string usuarioIdentityId)
        {
            var query = DbSet.AsNoTracking().AsQueryable();

            if (filtro.Periodo != null)
            {
                query = query.Where(x => x.Periodo == filtro.Periodo);
            }

            if (filtro.CategoriaId != null)
            {
                query = query.Where(x => x.CategoriaId == filtro.CategoriaId);
            }

            query = query.Where(x => x.UsuarioId == usuarioIdentityId);

            return await query.ToListAsync();
        }

        public async Task<LimiteOrcamento?> ObterLimiteOrcamentoCategoria(int idLimiteOrcamento)
        {
            return await DbSet.Include(x => x.Categoria).Where(x => x.Id == idLimiteOrcamento).FirstOrDefaultAsync();
        }
    }
}
