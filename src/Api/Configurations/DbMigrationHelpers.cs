using Business.Entities;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations
{
    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication application)
        {
            var service = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(service);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await EnsureSeedProduts(context);
            }
        }

        private static async Task EnsureSeedProduts(AppDbContext context)
        {   
            if (context.Users.Any() || context.Set<Usuario>().Any()) return;
           
            var userIdentity = new Usuario
            {
                Id = "1",
                Email = "teste@teste.com",
                EmailConfirmed = true,
                NormalizedEmail = "TESTE@TESTE.COM",
                UserName = "teste@teste.com",
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEF/nmfwFGPa8pnY9AvZL8HKI7r7l+aM4nryRB+Y3Ktgo6d5/0d25U2mhixnO4h/K5w==",
                NormalizedUserName = "TESTE@TESTE.COM",
                Nome = "Teste"
            };

            var categoria = new Categoria
            {
                Default = true,
                Nome = "Alimentação"
            };

            var transacao = new Transacao
            {
                UsuarioId = userIdentity.Id,
                CategoriaId = categoria.Id,
                Data = DateTime.Now,
                Tipo = TipoTransacao.Entrada,
                Descricao = "teste",
                Valor = 1000.59M,
                Categoria = categoria,
                Usuario = userIdentity,
            };

            await context.Users.AddAsync(userIdentity);

            await context.Set<Transacao>().AddAsync(transacao);

            await context.SaveChangesAsync();
        }
    }
}
