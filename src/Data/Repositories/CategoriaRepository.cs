using Business.Entities;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoriaRepository(AppDbContext dbContext) : Repository<Categoria>(dbContext), ICategoriaRepository
    {
        public async Task<Categoria?> ObterPorId(int id, string usuarioIdentityId)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id && x.Usuario.Id == usuarioIdentityId);
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasPadrao()
        {
            return await Buscar(x => x.Default);
        }
    }
}
