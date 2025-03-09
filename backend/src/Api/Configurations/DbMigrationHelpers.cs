using Business.Entities;
using Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations
{
    public static class DbMigrationHelpers
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            EnsureSeedData(app).Wait();
        }

        public static async Task EnsureSeedData(WebApplication application)
        {
            var service = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(service);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();
            await InserirDadosIniciais(context);
        }

        private static async Task InserirDadosIniciais(AppDbContext context)
        {
            var dataAgora = DateTime.Now;
            var dataOnlyPrimeiroDiaDoMes = new DateOnly(dataAgora.Year, dataAgora.Month, 1);
            var dataPrimeiroDiaDoMes = new DateTime(dataAgora.Year, dataAgora.Month, 1);

            if (context.Users.Any() || context.Set<Usuario>().Any()) return;
           
            var userIdentity = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "teste@teste.com",
                EmailConfirmed = true,
                NormalizedEmail = "TESTE@TESTE.COM",
                UserName = "teste@teste.com",
                AccessFailedCount = 0,
                PasswordHash = "AQAAAAIAAYagAAAAEEIsmFcI2MWZaRj7qTXrXoXatvLIeahf+yFJbb3pAI6SbmCFjHJtKz1Nxv0XOvhuQQ==",
                NormalizedUserName = "TESTE@TESTE.COM",
            };

            var usuario = new Usuario()
            {
                Id = userIdentity.Id,
                Nome = "Teste"
            };

            var categoria1 = new Categoria
            {
                Default = true,
                Nome = "Alimentação"
            };

            var categoria2 = new Categoria
            {
                Default = true,
                Nome = "Saúde"
            };

            var categoria3 = new Categoria
            {
                Default = true,
                Nome = "Transporte"
            };

            var categoria4 = new Categoria
            {
                Default = true,
                Nome = "Salário"
            };

            var categoria5 = new Categoria
            {
                Default = false,
                Nome = "Lazer",
                Usuario = usuario,
                UsuarioId = usuario.Id

            };

            var limite1 = new LimiteOrcamento()
            {
                Periodo = dataOnlyPrimeiroDiaDoMes,
                TipoLimite = TipoLimite.Geral,
                Limite = 2000,
                PorcentagemAviso = 50,
                UsuarioId = userIdentity.Id
            };

            var limite2 = new LimiteOrcamento()
            {
                Periodo = dataOnlyPrimeiroDiaDoMes,
                TipoLimite = TipoLimite.Categoria,
                CategoriaId = categoria1.Id, //Alimentação
                Limite = 1000,
                PorcentagemAviso = 50,
                UsuarioId = userIdentity.Id
            };

            var limite3 = new LimiteOrcamento()
            {
                Periodo = dataOnlyPrimeiroDiaDoMes,
                TipoLimite = TipoLimite.Categoria,
                CategoriaId = categoria5.Id, //Lazer
                Limite = 200,
                PorcentagemAviso = 50,
                UsuarioId = userIdentity.Id
            };

            var limite4 = new LimiteOrcamento()
            {
                Periodo = dataOnlyPrimeiroDiaDoMes,
                TipoLimite = TipoLimite.Categoria,
                CategoriaId = categoria2.Id, //Saúde
                Limite = 300,
                PorcentagemAviso = 50,
                UsuarioId = userIdentity.Id
            };

            var limite5 = new LimiteOrcamento()
            {
                Periodo = dataOnlyPrimeiroDiaDoMes,
                TipoLimite = TipoLimite.Categoria,
                CategoriaId = categoria3.Id, //Transporte
                Limite = 500,
                PorcentagemAviso = 50,
                UsuarioId = userIdentity.Id
            };

            var transacao1 = new Transacao
            {
                UsuarioId = userIdentity.Id,
                CategoriaId = categoria4.Id,
                Data = dataPrimeiroDiaDoMes,
                Tipo = TipoTransacao.Entrada,
                Descricao = "Remuneração fixa mensal",
                Valor = 2000,
                Categoria = categoria4,
                Usuario = usuario,
            };

            var transacao2 = new Transacao
            {
                UsuarioId = userIdentity.Id,
                CategoriaId = categoria1.Id,
                Data = dataPrimeiroDiaDoMes,
                Tipo = TipoTransacao.Saida,
                Descricao = "Compras de supermercado",
                Valor = 435.72M,
                Categoria = categoria1,
                Usuario = usuario,
            };

            var transacao4 = new Transacao
            {
                UsuarioId = userIdentity.Id,
                CategoriaId = categoria2.Id,
                Data = dataPrimeiroDiaDoMes,
                Tipo = TipoTransacao.Saida,
                Descricao = "Plano de saúde",
                Valor = 215.37M,
                Categoria = categoria2,
                Usuario = usuario,
            };

            var transacao5 = new Transacao
            {
                UsuarioId = userIdentity.Id,
                CategoriaId = categoria3.Id,
                Data = dataPrimeiroDiaDoMes,
                Tipo = TipoTransacao.Saida,
                Descricao = "Uber - ida ao trabalho",
                Valor = 18.94M,
                Categoria = categoria3,
                Usuario = usuario,
            };

            var transacao6 = new Transacao
            {
                UsuarioId = userIdentity.Id,
                CategoriaId = categoria5.Id,
                Data = dataPrimeiroDiaDoMes,
                Tipo = TipoTransacao.Saida,
                Descricao = "Ida a praia",
                Valor = 120.57M,
                Categoria = categoria5,
                Usuario = usuario,
            };

            await context.Users.AddAsync(userIdentity);

            await context.Set<Usuario>().AddAsync(usuario);

            await context.Set<Categoria>().AddRangeAsync([categoria1, categoria2, categoria3, categoria4]);

            await context.Set<LimiteOrcamento>().AddRangeAsync([limite1, limite2, limite3, limite4, limite5]);

            await context.Set<Transacao>().AddRangeAsync([transacao1, transacao2, transacao4, transacao5, transacao6]);

            await context.SaveChangesAsync();
        }
    }
}
