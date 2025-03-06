using Business.Entities;
using Business.Entities.Validations;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Messages;
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
        public async Task<LimiteOrcamento?> ObterPorId(Guid id)
        {
            var limiteOrcamento = await limiteOrcamentoRepository.ObterPorId(id);

            if (limiteOrcamento == null)
            {
                Notificar(Mensagens.RegistroNaoEncontrado);
                return null;
            }

            if (!AcessoAutorizado(limiteOrcamento.UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return null;
            }

            return limiteOrcamento;
        }

        public async Task Adicionar(LimiteOrcamento limiteOrcamento)
        {
            if(!ExecutarValidacao(new LimiteOrcamentoValidation(), limiteOrcamento)) return;

            if (!limiteOrcamentoTransacao.TemRecursoDisponivel(limiteOrcamento.Limite))
            {
                Notificar(Mensagens.SemRecursoDisponivel);
                return;
            }
            if(!await ValidarPorTipoLimite(limiteOrcamento)) return;

            limiteOrcamento.UsuarioId = UsuarioId;

            await limiteOrcamentoRepository.Adicionar(limiteOrcamento);
        }

        public async Task Atualizar(LimiteOrcamento limiteOrcamento)
        {
            if(!ExecutarValidacao(new LimiteOrcamentoValidation(), limiteOrcamento)) return;

            var limiteOrcamentoBanco = await limiteOrcamentoRepository.ObterPorId(limiteOrcamento.Id);

            if (limiteOrcamentoBanco == null)
            {
                Notificar(Mensagens.RegistroNaoEncontrado);
                return;
            }

            if (!AcessoAutorizado(UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return;
            }

            if (!limiteOrcamentoTransacao.TemRecursoDisponivel(limiteOrcamento.Limite))
            {   
                Notificar(Mensagens.SemRecursoDisponivel);
                return;
            }

            if(!await ValidarPorTipoLimite(limiteOrcamento)) return;

            limiteOrcamentoBanco.TipoLimite = limiteOrcamento.TipoLimite;
            limiteOrcamentoBanco.Limite = limiteOrcamento.Limite;
            limiteOrcamentoBanco.CategoriaId = limiteOrcamento.CategoriaId;
            limiteOrcamentoBanco.Periodo = limiteOrcamento.Periodo;
            limiteOrcamentoBanco.PorcentagemAviso = limiteOrcamento.PorcentagemAviso;

            await limiteOrcamentoRepository.Atualizar(limiteOrcamentoBanco);
        }
        public async Task Excluir(Guid id)
        {
            var entity = await limiteOrcamentoRepository.ObterPorId(id);

            if (entity == null)
            {   
                Notificar(Mensagens.RegistroNaoEncontrado);
                return;
            }

            await limiteOrcamentoRepository.Excluir(entity);
        }

        private async Task<bool> ValidarPorTipoLimite(LimiteOrcamento limiteOrcamento)
        {
            if (limiteOrcamento.TipoLimite == TipoLimite.Geral)
            {
                return ValidarLimiteGeral(limiteOrcamento.Periodo, limiteOrcamento.Id);
            }

            return await ValidarLimitePorCategoria(limiteOrcamento);
        }

        private bool ValidarLimiteGeral(DateOnly periodo, Guid idLimite)
        {
            if (!ExisteLimiteGeral(periodo, idLimite)) return true;

            Notificar(Mensagens.ExisteLimiteGeral);
            return false;
        }

        private async Task<bool> ValidarLimitePorCategoria(LimiteOrcamento limiteOrcamento)
        {
            await categoriaService.ObterPorId((Guid)limiteOrcamento.CategoriaId!);

            return !TemNotificacao();
        }

        private bool ExisteLimiteGeral(DateOnly periodo, Guid idLimite)
        {
            return limiteOrcamentoRepository.Buscar(predicate: x => x.UsuarioId == UsuarioId && x.Periodo == periodo 
                                                    && x.CategoriaId == null && x.TipoLimite == TipoLimite.Geral && x.Id != idLimite,
                                                                        orderBy: x => x.CategoriaId).Result.Any();
        }
    }
}
