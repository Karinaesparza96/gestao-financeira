using Business.Entities;
using Business.Interfaces;
using FluentValidation;

namespace Business.Services.Base
{
    public abstract class BaseService(IAppIdentityUser appIdentityUser, IUsuarioService usuarioService)
    {
        private readonly IAppIdentityUser _appIdentityUser = appIdentityUser;
        private readonly IUsuarioService _usuarioService = usuarioService;
        protected string UsuarioId => _appIdentityUser.GetUserId();
        protected ResultadoOperacao<TE> ExecutarValidacao<TV, TE>(TV validador, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var entidadeValidada = validador.Validate(entity);

            if (entidadeValidada.IsValid)
            {
                return ResultadoOperacao<TE>.Sucesso(entity);
            }

            return ResultadoOperacao<TE>.Falha(entidadeValidada.Errors.Select(e => e.ErrorMessage));
        }
        protected async Task<Usuario?> ObterUsuarioLogado()
        {
           return await _usuarioService.ObterUsuarioPorId(UsuarioId);
        }

        protected bool AcessoAutorizado(string? usuarioId)
        {
            return _appIdentityUser.IsOwner(usuarioId);
        }
    }
}
