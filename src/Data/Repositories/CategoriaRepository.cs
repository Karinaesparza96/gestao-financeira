using Business.Entities;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoriaRepository(AppDbContext dbContext) : Repository<Categoria>(dbContext), ICategoriaRepository
    {
        public async Task<Categoria?> ObterTransacoes(int id)
        {
            return await DbSet.Include(x => x.Transacoes).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
