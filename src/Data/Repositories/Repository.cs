using Business.Entities;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {   
        private readonly AppDbContext _dbContext;
        protected DbSet<TEntity> DbSet { get; set; }
        protected Repository(AppDbContext dbContext) 
        { 
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }
        public async Task<List<TEntity>> ObterTodos()
        {
           return await DbSet.ToListAsync();
        }

        public async Task<TEntity?> ObterPorId(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Excluir(TEntity entity)
        {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
