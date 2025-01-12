using Business.Entities;
using Business.Entities.Validations;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class LimiteOrcamentoService(IAppIdentityUser appIdentityUser, 
                                        ILimiteOrcamentoRepository limiteOrcamentoRepository,
                                        ILimiteOrcamentoTransacaoService limiteOrcamentoTransacao,
                                        INotificador notificador,
                                        ICategoriaService categoriaService) : BaseService(appIdentityUser, notificador), ILimiteOrcamentoService
    {
        public async Task<IEnumerable<LimiteOrcamento>> ObterTodos(FiltroLimiteOrcamento filtro)
        {
            var limitesOrcamentos = await limiteOrcamentoRepository.ObterTodos(filtro, UsuarioId);

            return limitesOrcamentos;
        }
        public async Task<LimiteOrcamento?> ObterPorId(int id)
        {
            var limiteOrcamento = await limiteOrcamentoRepository.ObterPorId(id);

            if (limiteOrcamento == null)
            {
                Notificar("Registro não encontrado");
                return null;
            }

            if (!AcessoAutorizado(limiteOrcamento.UsuarioId))
            {
                Notificar("Não é possivel acessar um registro de outro usuário.");
                return null;
            }

            return limiteOrcamento;
        }

        public async Task Adicionar(LimiteOrcamento limiteOrcamento)
        {
            if(!ExecutarValidacao(new LimiteOrcamentoValidation(), limiteOrcamento)) return;

            if (!limiteOrcamentoTransacao.TemRecursoDisponivel(limiteOrcamento.Limite))
            {
                Notificar("Não é possivel definir um limite que excede os recursos disponíveis.");
                return;
            }
            if(!await ValidarPorTipoLimite(limiteOrcamento)) return;

            limiteOrcamento.CategoriaId = limiteOrcamento.CategoriaId;
            limiteOrcamento.UsuarioId = UsuarioId;

            await limiteOrcamentoRepository.Adicionar(limiteOrcamento);
        }

        public async Task Atualizar(LimiteOrcamento limiteOrcamento)
        {
            if(!ExecutarValidacao(new LimiteOrcamentoValidation(), limiteOrcamento)) return;

            var limiteOrcamentoBanco = await limiteOrcamentoRepository.ObterPorId(limiteOrcamento.Id);

            if (limiteOrcamentoBanco == null)
            {
                Notificar("Registro não encontrado.");
                return;
            }

            if (!AcessoAutorizado(UsuarioId))
            {
                Notificar("Não é possível atualizar um registro de outro usuário.");
                return;
            }

            if (!limiteOrcamentoTransacao.TemRecursoDisponivel(limiteOrcamento.Limite))
            {   
                Notificar("Não é possivel definir um limite que excede os recursos disponíveis.");
                return;
            }

            if(!await ValidarPorTipoLimite(limiteOrcamento)) return;

            limiteOrcamentoBanco.Limite = limiteOrcamento.Limite;
            limiteOrcamentoBanco.CategoriaId = limiteOrcamento.CategoriaId;
            limiteOrcamentoBanco.Periodo = limiteOrcamento.Periodo;

            await limiteOrcamentoRepository.Atualizar(limiteOrcamentoBanco);
        }
        public async Task Exluir(int id)
        {
            var entity = await limiteOrcamentoRepository.ObterPorId(id);

            if (entity == null)
            {   
                Notificar("Registro não encontrado");
                return;
            }

            await limiteOrcamentoRepository.Excluir(entity);
        }

        private async Task<bool> ValidarPorTipoLimite(LimiteOrcamento limiteOrcamento)
        {
            if (limiteOrcamento.LimiteGeral)
            {
                return ValidarLimiteGeral(limiteOrcamento.Periodo);
            }

            return await ValidarLimitePorCategoria(limiteOrcamento);
        }

        private bool ValidarLimiteGeral(DateOnly periodo)
        {
            if (!ExisteLimiteGeral(periodo)) return true;

            Notificar("Já existe um limite geral definido para este período.");
            return false;
        }

        private async Task<bool> ValidarLimitePorCategoria(LimiteOrcamento limiteOrcamento)
        {
            await categoriaService.ObterPorId((int)limiteOrcamento.CategoriaId!);

            return !TemNotificacao();
        }

        private bool ExisteLimiteGeral(DateOnly periodo)
        {
            return limiteOrcamentoRepository.Buscar(predicate: x => x.UsuarioId == UsuarioId && x.Periodo == periodo && x.CategoriaId == null,
                                                                        orderBy: x => x.CategoriaId).Result.Any();
        }
    }
}
