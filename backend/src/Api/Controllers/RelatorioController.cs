using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Business.Utils;
using Api.Dtos;
using Business.Entities;
using Business.Interfaces;
using Business.FiltrosBusca;

namespace Api.Controllers;

//[Authorize]
[Route("api/relatorios")]
public class RelatorioController : Controller
{
    private readonly ICategoriaService _categoriaService;
    private readonly ITransacaoService _transacaoService;

    public RelatorioController(ICategoriaService categoriaService, ITransacaoService transacaoService)
    {
        _categoriaService = categoriaService;
        _transacaoService = transacaoService;
    }

    [HttpGet]
    public async Task<ActionResult> ObterResumoFinanceiro()
    {
        return Ok();
    }

    [HttpGet("categorias/csv")]
    public async Task<ActionResult<string>> ObterCategoriasCSV()
    {
        var categorias = await _categoriaService.ObterTodos();

        string lcCsvString = ExportHelper.getCSV<Categoria>(categorias.ToList());
        return Ok(ExportHelper.convertBase64(lcCsvString));
    }

    [HttpGet("transacoes/csv")]
    public async Task<ActionResult<string>> ObterTransacoesCSV([FromQuery] FiltroTransacao filtroDto)
    {
        var transacoes = await _transacaoService.ObterTodos(filtroDto);
        string lcCsvString = ExportHelper.getCSV<Transacao>(transacoes.ToList());
        return Ok(ExportHelper.convertBase64(lcCsvString));
    }
}
