namespace Business.Entities
{
    public class RetornoPadrao<TE> where TE : class
    {
        public TE? Value { get; set; }
        public IEnumerable<string> Erros { get; set; }

        public bool OperacaoValida {  get; set; }

        private RetornoPadrao(TE value)
        {
            Value = value;
            OperacaoValida = true;
            Erros = [];
        }

        private RetornoPadrao(IEnumerable<string> erros)
        {
            Value = default;
            OperacaoValida = false;
            Erros = erros;
        }

        public static RetornoPadrao<TE> Sucesso(TE value)
        {
            return new RetornoPadrao<TE>(value);
        }

        public static RetornoPadrao<TE> Falha(IEnumerable<string> erros)
        {
            return new RetornoPadrao<TE>(erros);
        }

        public static RetornoPadrao<TE> Falha(string erro)
        {
            return new RetornoPadrao<TE>([erro]);
        }
    }
}
