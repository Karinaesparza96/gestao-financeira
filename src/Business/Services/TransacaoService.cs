using Business.Entities;
using Business.Entities.Validations;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Notificacoes;
using Business.Services.Base;

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

        public async Task<Transacao?> ObterPorId(int id)
        {
            var transacao = await transacaoRepository.ObterPorId(id);

            if (transacao == null)
            {
                Notificar("Registro não encontrado.", TipoNotificacao.Aviso);
                return null;
            }

            if (!AcessoAutorizado(transacao.UsuarioId))
            {
                Notificar("Não é possível acessar o registro de outro usuário.");
                return null;
            }

            return transacao;
        }

        public async Task Adicionar(Transacao transacao)
        {
            if(!ExecutarValidacao(new TransacaoValidation(), transacao)) return;

            if (transacao.Tipo == TipoTransacao.Saida)
            {
                transacao.Valor = -transacao.Valor;
            }

            var categoria = await categoriaRepository.ObterPorId(transacao.CategoriaId);

            if (categoria == null)
            {
                Notificar("Categoria precisa ser cadastrada antes de associar a uma transação.");
                return;
            }

            transacao.Categoria = categoria;
            transacao.UsuarioId = UsuarioId;

            await transacaoRepository.Adicionar(transacao);
            await limiteOrcamentoTransacaoService.ValidarLimiteExcedido(UsuarioId, DateOnly.FromDateTime(transacao.Data));
        }

        public async Task Atualizar(Transacao transacao)
        {
            if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return;

            var transacaoBanco = await transacaoRepository.ObterPorId(transacao.Id);

            if (transacaoBanco == null)
            {
                Notificar("Registro não encontrado.");
                return;
            }

            if (!AcessoAutorizado(transacaoBanco.UsuarioId))
            {
                Notificar("Não é possível atualizar o registro de outro usuário.");
                return;
            }

            if (transacao.Tipo == TipoTransacao.Saida)
            {
                transacao.Valor = -transacao.Valor;
            }

            transacaoBanco.Tipo = transacao.Tipo;
            transacaoBanco.Valor = transacao.Valor;
            transacaoBanco.Data =  transacao.Data;
            transacaoBanco.Categoria = transacao.Categoria;
            transacaoBanco.Descricao = transacao.Descricao;

            await transacaoRepository.Atualizar(transacaoBanco);
        }

        public async Task Exluir(int id)
        {   
            var transacaoBanco = await transacaoRepository.ObterPorId(id);

            if (transacaoBanco == null)
            {
                Notificar("Registro não encontrado.");
                return;
            }

            if (!AcessoAutorizado(transacaoBanco.UsuarioId))
            {
                Notificar("Não é possivel excluir registro de outro usuário.");
                return;
            }

            await transacaoRepository.Excluir(transacaoBanco);
        }
    }
}
