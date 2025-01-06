using FluentValidation;

namespace Business.Entities.Validations
{
    public class LimiteOrcamentoValidation : AbstractValidator<LimiteOrcamento>
    {
        public LimiteOrcamentoValidation()
        {
            RuleFor(x => x.Limite)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido.")
                .GreaterThan(0).WithMessage("O campo {PropertyName} deve ser maior que {ComparisonValue}.");

            RuleFor(x => x.Periodo)
                .NotEmpty().WithMessage("O campo {PropertyName} deve ser fornecido.");

            RuleFor(x => x.PorcentagemAviso)
                .InclusiveBetween(0.01, 100)
                .WithMessage("O campo {PropertyName} deve estar entre {From} e {To}.");

        }
    }
}
