using Business.Entities;
using Business.Entities.Validations;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class LimiteOrcamentoService(IAppIdentityUser appIdentityUser, 
                                        ILimiteOrcamentoRepository limiteOrcamentoRepository,
                                        ILimiteOrcamentoTransacaoService limiteOrcamentoTransacaoService,
                                        ICategoriaService categoriaService,
                                        IUsuarioService usuarioService) : BaseService(appIdentityUser, usuarioService), ILimiteOrcamentoService
    {
        public async Task<IEnumerable<LimiteOrcamento>> ObterTodos(FiltroLimiteOrcamento filtro)
        {
            var limitesOrcamentos = await limiteOrcamentoRepository.ObterTodos(filtro, UsuarioId);

            return limitesOrcamentos;
        }
        public async Task<ResultadoOperacao<LimiteOrcamento>> ObterPorId(int id)
        {
            var limiteOrcamento = await limiteOrcamentoRepository.ObterPorId(id);

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

            if (!limiteOrcamentoTransacaoService.TemRecursoDisponivel(limiteOrcamento.Limite))
            {
                return ResultadoOperacao.Falha("Não é possivel definir um limite que excede os recursos disponíveis.");
            }
            var validacaoLimite = await ValidarPorTipoLimite(limiteOrcamento);

            if (!validacaoLimite.OperacaoValida)
                return validacaoLimite;

            limiteOrcamento.Usuario = (await ObterUsuarioLogado())!;

            await limiteOrcamentoRepository.Adicionar(limiteOrcamento);
            return ResultadoOperacao.Sucesso();
        }

        public async Task<ResultadoOperacao> Atualizar(LimiteOrcamento limiteOrcamento)
        {
            var validacaoEntidade = ExecutarValidacao(new LimiteOrcamentoValidation(), limiteOrcamento);

            if (!validacaoEntidade.OperacaoValida)
                return ResultadoOperacao.Falha(validacaoEntidade.Erros);

            var limiteOrcamentoBanco = await limiteOrcamentoRepository.ObterPorId(limiteOrcamento.Id);

            if (limiteOrcamentoBanco == null)
            {
                return ResultadoOperacao.Falha("Registro não encontrado.");
            }

            if (!AcessoAutorizado(UsuarioId))
            {
                return ResultadoOperacao.Falha("Não é possivel atualizar um registro de outro usuário.");
            }

            if (!limiteOrcamentoTransacaoService.TemRecursoDisponivel(limiteOrcamento.Limite))
            {
                return ResultadoOperacao.Falha("Não é possivel definir um limite que excede os recursos disponíveis.");
            }
            var validacaoLimite = await ValidarPorTipoLimite(limiteOrcamento);

            if (!validacaoLimite.OperacaoValida)
                return validacaoLimite;

            limiteOrcamentoBanco.Limite = limiteOrcamento.Limite;
            limiteOrcamentoBanco.CategoriaId = limiteOrcamento.CategoriaId;
            limiteOrcamentoBanco.Periodo = limiteOrcamento.Periodo;

            await limiteOrcamentoRepository.Atualizar(limiteOrcamentoBanco);
            return ResultadoOperacao.Sucesso();
        }
        public async Task<ResultadoOperacao> Exluir(int id)
        {
            var entity = await limiteOrcamentoRepository.ObterPorId(id);

            if (entity == null) return ResultadoOperacao.Falha("Registro não encontrado");

            await limiteOrcamentoRepository.Excluir(entity);

            return ResultadoOperacao.Sucesso();
        }

        private async Task<ResultadoOperacao> ValidarPorTipoLimite(LimiteOrcamento limiteOrcamento)
        {
            if (limiteOrcamento.LimiteGeral)
            {
                return ValidarLimiteGeral(limiteOrcamento.Periodo);
            }

            return await ValidarEAtribuirLimitePorCategoria(limiteOrcamento);
        }

        private ResultadoOperacao ValidarLimiteGeral(DateOnly periodo)
        {
            return ExisteLimiteGeral(periodo) ? ResultadoOperacao.Falha("Já existe um limite geral definido para este período.") : ResultadoOperacao.Sucesso();
        }

        private async Task<ResultadoOperacao> ValidarEAtribuirLimitePorCategoria(LimiteOrcamento limiteOrcamento)
        {
            var validaCategoria = await categoriaService.ObterPorId((int)limiteOrcamento.CategoriaId!);

            if (!validaCategoria.OperacaoValida)
            {
                return ResultadoOperacao.Falha(validaCategoria.Erros);
            }

            limiteOrcamento.Categoria = validaCategoria.Data;
            return ResultadoOperacao.Sucesso();
        }

        private bool ExisteLimiteGeral(DateOnly periodo)
        {
            return limiteOrcamentoRepository.Buscar(predicate: x => x.UsuarioId == UsuarioId && x.Periodo == periodo && x.CategoriaId == null,
                                                                        orderBy: x => x.CategoriaId).Result.Any();
        }
    }
}
