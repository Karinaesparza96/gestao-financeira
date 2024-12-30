using Business.Interfaces;
using Business.Notificacoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Api.Controllers.Base
{
    [ApiController]
    public abstract class MainController(INotificador notificador) : ControllerBase
    {   
        private readonly INotificador _notificador = notificador;
        protected ActionResult RetornoPadrao(HttpStatusCode statusCode = HttpStatusCode.OK, object? data = null)
        {
            if (OperacaoValida())
            {
                return new ObjectResult(data ?? new { })
                {
                    StatusCode = (int)statusCode,
                };
            }
            return BadRequest(new
            {
                erros = _notificador.ObterTodos().Select(x => x.Mensagem)
            });
        }

        protected ActionResult RetornoPadrao(ModelStateDictionary modelstate)
        {   
            if (!modelstate.IsValid)
            {
               NotificarErros(modelstate);
            }
            return RetornoPadrao();
        }

        protected ActionResult RetornoPadrao(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                NotificarErros(identityResult);
            }
            return RetornoPadrao();
        }


        protected void NotificarErros(ModelStateDictionary modelstate)
        {
            foreach (var e in modelstate.Values.SelectMany(e => e.Errors))
            {
                var msg = e.Exception != null ? e.Exception.Message : e.ErrorMessage;
                NotificarErros(msg);
            }
        }

        protected void NotificarErros(IdentityResult IdentityResult)
        {
            foreach (var e in IdentityResult.Errors)
            {
                var msg = e.Description;
                NotificarErros(msg);
            }
        }

        protected void NotificarErros(string mensagemErro)
        {
            _notificador.Adicionar(new Notificacao(mensagemErro));
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

    }
}
