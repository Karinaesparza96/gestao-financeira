using Business.Dtos;
using Business.Entities;
using Business.Entities.Validations;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class TransacaoService(ITransacaoRepository repository, 
                                IAppIdentityUser appIdentityUser, 
                                ICategoriaRepository categoriaRepository,
                                IUsuarioService usuarioService,
                                INotificador notificador) : BaseService(appIdentityUser, notificador, usuarioService), ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository = repository;
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        public async Task<IEnumerable<Transacao>> ObterTodos(FiltroTransacaoDto filtroDto)
        {
            var transacoesUsuario = await _transacaoRepository.ObterTodos(filtroDto, ObterUsuarioId());

            return transacoesUsuario;
        }

        public async Task<Transacao?> ObterPorId(int id)
        {
            var transacao = await _transacaoRepository.ObterPorId(id);

            var usuarioAutorizado = _appIdentityUser.IsOwner(transacao?.UsuarioId);

            return usuarioAutorizado ? transacao : null;
        }

        public async Task Adicionar(Transacao entity)
        {
            if (!ExecutarValidacao(new TransacaoValidation(), entity)) return;

            var usuario = await ObterUsuarioLogado();

            if (usuario == null)
            {
                NotificarErro("Usuário não encontrado.");
                return;
            }

            if (entity.Tipo == TipoTransacao.Saida)
            {
                entity.Valor = -entity.Valor;
            }

            var categoria = await _categoriaRepository.ObterPorId(entity.CategoriaId);

            if (categoria == null)
            {
                NotificarErro("Categoria precisa ser cadastrada antes de associar a uma transação.");
                return;
            }

            entity.Categoria = categoria;
            entity.Usuario = usuario;

            await _transacaoRepository.Adicionar(entity);
        }

        public async Task Atualizar(Transacao transacao)
        {
            if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return;

            var transacaoBanco = await _transacaoRepository.ObterPorId(transacao.Id);

            if (transacaoBanco == null)
            {
                NotificarErro("Registro não encontrado.");
                return;
            }

            var usuarioAutorizado = _appIdentityUser.IsOwner(transacaoBanco.UsuarioId);

            if (!usuarioAutorizado)
            {
                NotificarErro("Não é possivel atualizar registro de outro usuário.");
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

            await _transacaoRepository.Atualizar(transacaoBanco);
        }

        public async Task Exluir(int id)
        {   
            var transacaoBanco = await _transacaoRepository.ObterPorId(id);

            if (transacaoBanco == null)
            {
                NotificarErro("Registro não encontrado.");
                return;
            }
            var usuarioAutorizado = _appIdentityUser.IsOwner(transacaoBanco.UsuarioId);

            if (!usuarioAutorizado)
            {
                NotificarErro("Não é possivel excluir registro de outro usuário.");
                return;
            }

            await _transacaoRepository.Excluir(transacaoBanco);
        }
    }
}
