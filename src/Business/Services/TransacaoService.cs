using AutoMapper;
using Business.Entities;
using Business.Entities.Validations;
using Business.FiltrosBusca;
using Business.Interfaces;
using Business.Services.Base;

namespace Business.Services
{
    public class TransacaoService(ITransacaoRepository repository, 
                                IAppIdentityUser appIdentityUser, 
                                ICategoriaRepository categoriaRepository,
                                IMapper mapper,
                                IUsuarioService usuarioService) : BaseService(appIdentityUser, usuarioService), ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository = repository;
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        public async Task<ResultadoOperacao<IEnumerable<Transacao>>> ObterTodos(FiltroTransacao filtroDto)
        {
            var transacoesUsuario = await _transacaoRepository.ObterTodos(filtroDto, UsuarioId);
            var transacoesUsuarioDto = mapper.Map<IEnumerable<Transacao>>(transacoesUsuario);

            return ResultadoOperacao<IEnumerable<Transacao>>.Sucesso(transacoesUsuarioDto);
        }

        public async Task<ResultadoOperacao<Transacao>> ObterPorId(int id)
        {
            var transacao = await _transacaoRepository.ObterPorId(id);

            if (transacao == null)
            {
                return ResultadoOperacao<Transacao>.Falha("Registro não encontrado.");
            }

            if (!AcessoAutorizado(transacao.UsuarioId))
            {
                return ResultadoOperacao<Transacao>.Falha("Não é possível acessar o registro de outro usuário.");
            }
            var transacaoDto = mapper.Map<Transacao>(transacao);
            return ResultadoOperacao<Transacao>.Sucesso(transacaoDto);
        }

        public async Task<ResultadoOperacao> Adicionar(Transacao transacao)
        {
            var result = ExecutarValidacao(new TransacaoValidation(), transacao);
            if (!result.OperacaoValida) return ResultadoOperacao.Falha(result.Erros);

            var usuario = await ObterUsuarioLogado();

            if (usuario == null)
            {
                return ResultadoOperacao.Falha("Usuário não encontrado.");
            }

            if (transacao.Tipo == TipoTransacao.Saida)
            {
                transacao.Valor = -transacao.Valor;
            }

            var categoria = await _categoriaRepository.ObterPorId(transacao.CategoriaId);

            if (categoria == null)
            {
                return ResultadoOperacao.Falha("Categoria precisa ser cadastrada antes de associar a uma transação.");
            }

            transacao.Categoria = categoria;
            transacao.Usuario = usuario;

            await _transacaoRepository.Adicionar(transacao);
            var limiteExcedido = await _transacaoRepository.VerificarLimiteExcedido(UsuarioId, DateOnly.FromDateTime(transacao.Data));

            if (limiteExcedido)
            {
                return ResultadoOperacao.Sucesso(new Mensagem("Você excedeu um limite de orçamento definido."));
            }
            return ResultadoOperacao.Sucesso();
        }

        public async Task<ResultadoOperacao> Atualizar(Transacao transacao)
        {
            var result = ExecutarValidacao(new TransacaoValidation(), transacao);
            if (!result.OperacaoValida) return ResultadoOperacao.Falha(result.Erros);

            var transacaoBanco = await _transacaoRepository.ObterPorId(transacao.Id);

            if (transacaoBanco == null)
            {
                return ResultadoOperacao.Falha("Registro não encontrado.");
            }

            if (!AcessoAutorizado(transacaoBanco.UsuarioId))
            {
                return ResultadoOperacao.Falha("Não é possivel atualizar registro de outro usuário.");
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
            return ResultadoOperacao.Sucesso();
        }

        public async Task<ResultadoOperacao> Exluir(int id)
        {   
            var transacaoBanco = await _transacaoRepository.ObterPorId(id);

            if (transacaoBanco == null)
            {
                return ResultadoOperacao.Falha("Registro não encontrado.");
            }

            if (!AcessoAutorizado(transacaoBanco.UsuarioId))
            {
                return ResultadoOperacao.Falha("Não é possivel excluir registro de outro usuário.");
            }

            await _transacaoRepository.Excluir(transacaoBanco);
            return ResultadoOperacao.Sucesso();
        }
    }
}
