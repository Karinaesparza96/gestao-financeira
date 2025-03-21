﻿using System.ComponentModel;
using System.Net;
using Api.Controllers.Base;
using Api.Dtos;
using AutoMapper;
using Business.Entities;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/limites-orcamentos")]
    public class LimiteOrcamentoController(ILimiteOrcamentoService limiteOrcamentoService, IMapper mapper, INotificador notificador) : MainController(notificador)
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LimiteOrcamentoDto>>> ObterTodos([FromQuery]FiltroLimiteOrcamento filtroLimiteOrcamento)
        {
            var limiteOrcamentos = await limiteOrcamentoService.ObterTodos(filtroLimiteOrcamento);
            var limitesOrcamentos = mapper.Map<IEnumerable<LimiteOrcamentoUtilizadoDto>>(limiteOrcamentos);
            PreencherLimiteUtilizadoDto(limitesOrcamentos);
            return RetornoPadrao(data: limitesOrcamentos);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<LimiteOrcamentoDto>> ObterPorId(Guid id)
        {
            var limiteOrcamento = await limiteOrcamentoService.ObterPorId(id);
            return RetornoPadrao(data: mapper.Map<LimiteOrcamentoDto>(limiteOrcamento));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(LimiteOrcamentoDto limiteOrcamentoDto)
        {
            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await limiteOrcamentoService.Adicionar(mapper.Map<LimiteOrcamento>(limiteOrcamentoDto));
            return RetornoPadrao(HttpStatusCode.Created);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Atualizar(Guid id, LimiteOrcamentoDto limiteOrcamentoDto)
        {
            if (id != limiteOrcamentoDto.Id)
            {
                NotificarErro(Mensagens.IdsDiferentes);
                return RetornoPadrao();
            }

            if (!ModelState.IsValid)
            {
                NotificarErro(ModelState);
                return RetornoPadrao();
            }

            await limiteOrcamentoService.Atualizar(mapper.Map<LimiteOrcamento>(limiteOrcamentoDto));
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Excluir(Guid id)
        {
            await limiteOrcamentoService.Excluir(id);
            return RetornoPadrao(HttpStatusCode.NoContent);
        }

        private void PreencherLimiteUtilizadoDto(IEnumerable<LimiteOrcamentoUtilizadoDto> limitesOrcamentoUtilizado)
        {
            foreach (var limite in limitesOrcamentoUtilizado)
            {
                var limiteUtilizado = limiteOrcamentoService.ObterValorTotalDeSaidasNoPeriodo(limite.UsuarioId, limite.Periodo, limite.CategoriaId);
                if (limiteUtilizado != 0)
                {
                    limite.LimiteUtilizado = limiteUtilizado;
                    var percentualUtilizado = (limiteUtilizado / limite.Limite) * Convert.ToDecimal(100);
                    limite.PercentualLimiteUtilizado = percentualUtilizado.ToString("##0.00").Replace(".", ",") + "%";
                }
                else
                {
                    limite.LimiteUtilizado = 0;
                    limite.PercentualLimiteUtilizado = "0,00%";
                }
            }
        }
    }
}
