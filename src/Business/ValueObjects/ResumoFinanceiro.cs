namespace Business.ValueObjects;

public class ResumoFinanceiro
{
    public decimal TotalSaldo => TotalReceita - TotalDespesa;

    public decimal TotalReceita { get; set; }

    public decimal TotalDespesa { get; set; }

}