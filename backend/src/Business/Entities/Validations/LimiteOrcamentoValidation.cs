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
                .InclusiveBetween(0.01m, 100)
                .WithMessage("O campo {PropertyName} deve estar entre {From} e {To}.");

            RuleFor(x => x.TipoLimite)
                .IsInEnum()
                .WithMessage("O campo {PropertyName} deve ser um valor válido: Geral (1) ou Categoria (2).");

            When(x => x.TipoLimite == TipoLimite.Geral, () =>
                {
                    RuleFor(x => x.CategoriaId)
                        .Null()
                        .WithMessage("Para definir um limite geral o {PropertyName} não pode conter valor.");
                }
            );

        }
    }
}
