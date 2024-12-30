using FluentValidation;

namespace Business.Entities.Validations
{
    public class LimiteOrcamentoMapping : AbstractValidator<LimiteOrcamento>
    {
        public LimiteOrcamentoMapping()
        {
            RuleFor(x => x.Limite)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido.")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}.");

            RuleFor(x => x.Periodo)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido.");
        }
    }
}
