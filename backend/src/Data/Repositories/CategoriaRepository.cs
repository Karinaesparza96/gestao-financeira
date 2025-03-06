using Business.Entities;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoriaRepository(AppDbContext dbContext) : Repository<Categoria>(dbContext), ICategoriaRepository
    {
        public async Task<Categoria?> ObterTransacoes(Guid id)
        {
            return await DbSet.Include(x => x.Transacoes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Categoria?> ObterPorNomeEUsuario(string nome, string usuarioId)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Nome == nome && (c.Default || c.UsuarioId == usuarioId));
        }
    }
}
