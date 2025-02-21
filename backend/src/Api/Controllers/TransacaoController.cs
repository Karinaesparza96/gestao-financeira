using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Business.Messages;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/transacoes")]
    public class TransacaoController(ITransacaoService transacaoService, IMapper mapper, INotificador notificador) : MainController(notificador)
    {   
        
        [HttpGet]
        public async Task<ActionResult> ObterTodos([FromQuery]FiltroTransacao filtroDto)
        {
            var transacaos = await transacaoService.ObterTodos(filtroDto);

            return RetornoPadrao(data: mapper.Map<IEnumerable<TransacaoDto>>(transacaos));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> ObterPorId(Guid id)
        {   
            var transacao = await transacaoService.ObterPorId(id);

            return RetornoPadrao(data: mapper.Map<TransacaoDto>(transacao));
        }

        [HttpGet("resumo")]
        public async Task<ActionResult> ObterResumo()
        {
            var resumo = await transacaoService.ObterResumoEntradasESaidas();
            return RetornoPadrao(data: resumo);
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(TransacaoDto transacaoDto)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await transacaoService.Adicionar(mapper.Map<Transacao>(transacaoDto));

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Atualizar(Guid id, TransacaoDto transacaoDto)
        {
            if (id != transacaoDto.Id)
            {   
                NotificarErro(Mensagens.IdsDiferentes);
                return RetornoPadrao();
            }

            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await transacaoService.Atualizar(mapper.Map<Transacao>(transacaoDto));

            return RetornoPadrao(HttpStatusCode.Created);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            await transacaoService.Excluir(id);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }
    }
}
