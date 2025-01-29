using Business.Entities;
using Business.Interfaces;
using Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services.Base
{
    public abstract class BaseService(IAppIdentityUser appIdentityUser, INotificador notificador)
    {
        protected string UsuarioId => appIdentityUser.GetUserId();
        protected bool ExecutarValidacao<TV, TE>(TV validador, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var entidadeValidada = validador.Validate(entity);

            if (entidadeValidada.IsValid)
            {
                return true;
            }

            Notificar(entidadeValidada);

            return false;
        }

        protected bool AcessoAutorizado(string? usuarioId)
        {
            return appIdentityUser.IsOwner(usuarioId);
        }

        protected void Notificar(string mensagem, TipoNotificacao? tipo = TipoNotificacao.Erro)
        {
            notificador.Adicionar(new Notificacao(mensagem, tipo));
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected bool TemNotificacao()
        {
            return notificador.TemNotificacao();
        }
    }
}
