namespace Business.Entities
{
    public class ResultadoOperacao<T> where T : class
    {
        public T? Data { get; }
        private IEnumerable<string> _erros { get; set; } = [];

        private ResultadoOperacao(T? data = null)
        {
            Data = data;
        }

        private ResultadoOperacao(IEnumerable<string> erros)
        {
            Data = default;
            _erros = erros;
        }

        public bool OperacaoValida => !_erros.Any();

        public IEnumerable<string> Erros => _erros;

        public static ResultadoOperacao<T> Sucesso(T? data = null)
        {
            return new ResultadoOperacao<T>(data);
        }

        public static ResultadoOperacao<T> Falha(IEnumerable<string> erros)
        {
            return new ResultadoOperacao<T>(erros);
        }

        public static ResultadoOperacao<T> Falha(string erro)
        {
            return new ResultadoOperacao<T>([erro]);
        }
    }

    public class ResultadoOperacao
    {
        private IEnumerable<string> _erros { get; set; } = [];

        public Mensagem? Mensagem {  get; }

        private ResultadoOperacao(Mensagem? data = null)
        {
            Mensagem = data;
        }
        private ResultadoOperacao(IEnumerable<string> erros)
        {
            _erros = erros;
        }

        public bool OperacaoValida => !_erros.Any();

        public IEnumerable<string> Erros => _erros;

        public static ResultadoOperacao Sucesso(Mensagem? data = null)
        {
            return new ResultadoOperacao(data);
        }

        public static ResultadoOperacao Falha(IEnumerable<string> erros)
        {
            return new ResultadoOperacao(erros);
        }

        public static ResultadoOperacao Falha(string erro)
        {
            return new ResultadoOperacao([erro]);
        }
    }

    public class Mensagem(string descricao)
    {
        public string Descricao { get; } = descricao;
    }
}
