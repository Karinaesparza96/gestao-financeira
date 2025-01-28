using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ObterPorId(int id)
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, TransacaoDto transacaoDto)
        {
            if (id != transacaoDto.Id)
            {   
                NotificarErro("Os ids devem ser iguais.");
                return RetornoPadrao();
            }

            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await transacaoService.Atualizar(mapper.Map<Transacao>(transacaoDto));

            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await transacaoService.Exluir(id);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }
    }
}
