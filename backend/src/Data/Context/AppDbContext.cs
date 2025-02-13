using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> db) : IdentityDbContext(db)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(200)");

            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var relationShip in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationShip.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(builder);
        }
    }
}
