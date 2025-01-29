using FluentValidation;

namespace Business.Entities.Validations
{
    public class TransacaoValidation : AbstractValidator<Transacao>
    {
        public TransacaoValidation() 
        {
            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido.")
                .GreaterThan(-1).WithMessage("O campo {PropertyName} da transação não pode ser negativo.");

            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido.")
                .IsInEnum().WithMessage("O campo {PropertyName} fornecido não é válido.");

            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido.");

            RuleFor(x => x.Descricao)
                .Length(1, 200).WithMessage("O campo {PropertyName} precisa estar entre {MinLength} e {MaxLength}.");
        }
    }
}
