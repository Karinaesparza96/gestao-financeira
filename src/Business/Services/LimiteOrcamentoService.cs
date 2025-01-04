using Business.Entities;
using Business.Entities.Validations;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class LimiteOrcamentoService(IAppIdentityUser appIdentityUser, 
                                        ILimiteOrcamentoRepository limiteOrcamentoRepository,
                                        ITransacaoRepository transacaoRepository,
                                        ICategoriaService categoriaService,
                                        IUsuarioService usuarioService) : BaseService(appIdentityUser, usuarioService), ILimiteOrcamentoService
    {   
        private readonly ILimiteOrcamentoRepository _limiteOrcamentoRepository = limiteOrcamentoRepository;
        private readonly ITransacaoRepository _transacaoRepository = transacaoRepository;
        private readonly ICategoriaService _categoriaService = categoriaService;

        public async Task<IEnumerable<LimiteOrcamento>> ObterTodos(FiltroLimiteOrcamento filtro)
        {
            var limitesOrcamentos = await _limiteOrcamentoRepository.ObterTodos(filtro, UsuarioId);

            return limitesOrcamentos;
        }
        public async Task<ResultadoOperacao<LimiteOrcamento>> ObterPorId(int id)
        {
            var limiteOrcamento = await _limiteOrcamentoRepository.ObterPorId(id);

            if (limiteOrcamento == null)
            {
                return ResultadoOperacao<LimiteOrcamento>.Falha("Registro não encontrado");
            }

            if (!AcessoAutorizado(limiteOrcamento.UsuarioId))
            {
                return ResultadoOperacao<LimiteOrcamento>.Falha("Não é possivel acessar um registro de outro usuário.");
            }

            return ResultadoOperacao<LimiteOrcamento>.Sucesso(limiteOrcamento);
        }

        public async Task<ResultadoOperacao> Adicionar(LimiteOrcamento limiteOrcamento)
        {
            var validacaoEntidade = ExecutarValidacao(new LimiteOrcamentoValidation(), limiteOrcamento);

            if (!validacaoEntidade.OperacaoValida) 
                return ResultadoOperacao.Falha(validacaoEntidade.Erros);

            if (!TemRecursoDisponivel(limiteOrcamento.Limite))
            {
                return ResultadoOperacao.Falha("Não é possivel definir um limite que excede os recursos disponíveis.");
            }
            var validacaoLimite = await ValidarPorTipoLimite(limiteOrcamento);

            if (!validacaoLimite.OperacaoValida)
                return validacaoLimite;

            limiteOrcamento.Usuario = await ObterUsuarioLogado();

            await _limiteOrcamentoRepository.Adicionar(limiteOrcamento);
            return ResultadoOperacao.Sucesso();
        }
        public async Task<ResultadoOperacao> ValidarPorTipoLimite(LimiteOrcamento limiteOrcamento)
        {
            if (limiteOrcamento.LimiteGeral)
            {
                return ValidarLimiteGeral(limiteOrcamento.Periodo);
            }
            else
            {
                return await ValidarEAtribuirLimitePorCategoria(limiteOrcamento);
            }
        }

        public Task<ResultadoOperacao> Atualizar(LimiteOrcamento entityDto)
        {
            throw new NotImplementedException();
        }
        public async Task<ResultadoOperacao> Exluir(int id)
        {
            var entity = await _limiteOrcamentoRepository.ObterPorId(id);

            if (entity == null) return ResultadoOperacao.Falha("Registro não encontrado");

            await _limiteOrcamentoRepository.Excluir(entity);

            return ResultadoOperacao.Sucesso();
        }

        private ResultadoOperacao ValidarLimiteGeral(DateOnly periodo)
        {
            if (ExisteLimiteGeral(periodo))
            {
                return ResultadoOperacao.Falha("Já existe um limite geral definido para este período.");
            }

            return ResultadoOperacao.Sucesso();
        }

        private async Task<ResultadoOperacao> ValidarEAtribuirLimitePorCategoria(LimiteOrcamento limiteOrcamento)
        {
            var validaCategoria = await _categoriaService.ObterPorId((int)limiteOrcamento.CategoriaId);

            if (!validaCategoria.OperacaoValida)
            {
                return ResultadoOperacao.Falha(validaCategoria.Erros);
            }

            limiteOrcamento.Categoria = validaCategoria.Data;
            return ResultadoOperacao.Sucesso();
        }

        private bool TemRecursoDisponivel(decimal limite)
        {
            var saldoTotal = _transacaoRepository.ObterSaldoTotal(UsuarioId);

            return saldoTotal >= limite;
        }
        private bool ExisteLimiteGeral(DateOnly periodo)
        {
            return _limiteOrcamentoRepository.Buscar(predicate: x => x.UsuarioId == UsuarioId && x.Periodo == periodo && x.CategoriaId == null,
                                                                        orderBy: x => x.CategoriaId).Result.Any();
        }
    }
}
