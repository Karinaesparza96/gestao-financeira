using Business.Entities;
using Business.Entities.Validations;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Messages;
using Business.Services.Base;
using Business.ValueObjects;

namespace Business.Services
{
    public class TransacaoService(ITransacaoRepository transacaoRepository,
                                ILimiteOrcamentoTransacaoService limiteOrcamentoTransacaoService,
                                IAppIdentityUser appIdentityUser, 
                                ICategoriaRepository categoriaRepository,
                                INotificador notificador) : BaseService(appIdentityUser, notificador), ITransacaoService
    {
        public async Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacao filtroDto)
        {
            var transacoesUsuario = await transacaoRepository.ObterTodos(filtroDto, UsuarioId);

            return transacoesUsuario;
        }

        public async Task<Transacao?> ObterPorId(Guid id)
        {
            var transacao = await transacaoRepository.ObterPorId(id);

            if (transacao == null)
            {
                Notificar(Mensagens.RegistroNaoEncontrado);
                return null;
            }

            if (!AcessoAutorizado(transacao.UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return null;
            }

            return transacao;
        }

        public async Task<ResumoFinanceiro> ObterResumoEntradasESaidas()
        {
            return await transacaoRepository.ObterResumoEntradasESaidas(UsuarioId);
        }

        public async Task Adicionar(Transacao transacao)
        {
            if(!ExecutarValidacao(new TransacaoValidation(), transacao)) return;
            
            var categoria = await categoriaRepository.ObterPorId(transacao.CategoriaId);

            if (categoria == null)
            {
                Notificar(Mensagens.CategoriaNaoCadastrada);
                return;
            }

            transacao.Categoria = categoria;
            transacao.UsuarioId = UsuarioId;

            await transacaoRepository.Adicionar(transacao);
            await limiteOrcamentoTransacaoService.ValidarLimitesExcedido(UsuarioId, DateOnly.FromDateTime(transacao.Data));
        }

        public async Task Atualizar(Transacao transacao)
        {
            if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return;

            var transacaoBanco = await transacaoRepository.ObterPorId(transacao.Id);

            if (transacaoBanco == null)
            {
                Notificar(Mensagens.RegistroNaoEncontrado);
                return;
            }

            if (!AcessoAutorizado(transacaoBanco.UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return;
            }

            transacaoBanco.Tipo = transacao.Tipo;
            transacaoBanco.Valor = transacao.Valor;
            transacaoBanco.Data =  transacao.Data;
            transacaoBanco.Categoria = transacao.Categoria;
            transacaoBanco.Descricao = transacao.Descricao;

            await transacaoRepository.Atualizar(transacaoBanco);
            await limiteOrcamentoTransacaoService.ValidarLimitesExcedido(UsuarioId, DateOnly.FromDateTime(transacao.Data));
        }

        public async Task Excluir(Guid id)
        {   
            var transacaoBanco = await transacaoRepository.ObterPorId(id);

            if (transacaoBanco == null)
            {
                Notificar(Mensagens.RegistroNaoEncontrado);
                return;
            }

            if (!AcessoAutorizado(transacaoBanco.UsuarioId))
            {
                Notificar(Mensagens.AcaoNaoAutorizada);
                return;
            }

            await transacaoRepository.Excluir(transacaoBanco);
        }
    }
}
