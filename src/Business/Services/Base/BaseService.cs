using Business.Entities;
using Business.Interfaces;
using Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services.Base
{
    public abstract class BaseService(IAppIdentityUser appIdentityUser, INotificador notificador, IUsuarioService usuarioService)
    {
        protected readonly IAppIdentityUser _appIdentityUser = appIdentityUser;
        private readonly INotificador _noficador = notificador;
        private readonly IUsuarioService _usuarioService = usuarioService;

        protected bool ExecutarValidacao<TV, TE>(TV validador, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var entidadeValidada = validador.Validate(entity);

            if (entidadeValidada.IsValid) return true;

            NotificarErro(entidadeValidada);

            return false;
        }

        protected void NotificarErro(ValidationResult validation)
        {
            foreach (var erro in validation.Errors)
            {
                NotificarErro(erro.ErrorMessage);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _noficador.Adicionar(new Notificacao(mensagem));
        }

        protected string ObterUsuarioId()
        {
            return _appIdentityUser.GetUserId();
        }

        protected async Task<Usuario?> ObterUsuarioLogado()
        {
            var idUsuariologado = ObterUsuarioId();

           return await _usuarioService.ObterUsuarioPorId(idUsuariologado);
        }
    }
}
