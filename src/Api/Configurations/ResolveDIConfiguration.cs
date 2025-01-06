using Api.Extensions;
using Api.Services;
using Business.Interfaces;
using Business.Notificacoes;
using Business.Services;
using Data.Repositories;

namespace Api.Configurations
{
    public static class ResolveDIConfiguration
    {
        public static WebApplicationBuilder AddResolveDependencie(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<INotificador, Notificador>();

            builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<ILimiteOrcamentoRepository, LimiteOrcamentoRepository>();

            builder.Services.AddScoped<ITransacaoService, TransacaoService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<ILimiteOrcamentoService, LimiteOrcamentoService>();
            builder.Services.AddScoped<ILimiteOrcamentoTransacaoService, LimiteOrcamentoTransacaoService>();


            return builder;
        }
    }
}
