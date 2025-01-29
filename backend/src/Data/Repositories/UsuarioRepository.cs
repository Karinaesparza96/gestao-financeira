using Business.Entities;
using Business.Interfaces;
using Data.Context;

namespace Data.Repositories
{
    public class UsuarioRepository(AppDbContext dbContext) : Repository<Usuario>(dbContext), IUsuarioRepository
    {
    }
}
