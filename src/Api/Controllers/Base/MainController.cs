using System.Net;
using Api.Dtos;
using Business.Interfaces;
using Business.Notificacoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers.Base;

[ApiController]
public abstract class MainController(INotificador notificador) : ControllerBase
{
    protected ActionResult RetornoPadrao(HttpStatusCode statusCode = HttpStatusCode.OK, object? data = null)
    {
        if (!OperacaoValida())
            return BadRequest(new ResponseDefaultFalha
            {
                Erros = notificador.ObterPorTipo(TipoNotificacao.Erro).Select(x => x.Mensagem)!
            });

        return new ObjectResult(new ResponseDefaultSucesso
        {
            Data = data,
            Avisos = notificador.ObterPorTipo(TipoNotificacao.Aviso).Select(x => x.Mensagem)!
        })
        {
            StatusCode = (int)statusCode
        };
    }

    protected bool OperacaoValida()
    {
        return notificador.ObterPorTipo(TipoNotificacao.Erro).Count == 0;
    }

    protected void NotificarErro(string erro)
    {
        notificador.Adicionar(new Notificacao(erro));
    }

    protected void NotificarErro(ModelStateDictionary modelstate)
    {
        foreach (var msg in modelstate.Values
                     .SelectMany(e => e.Errors)
                     .Select(e => e.Exception?.Message ?? e.ErrorMessage))
            NotificarErro(msg);
    }

    protected void NotificarErro(IdentityResult identityResult)
    {
        foreach (var erro in identityResult.Errors.Select(e => e.Description)) NotificarErro(erro);
    }
}