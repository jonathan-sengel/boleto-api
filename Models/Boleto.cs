namespace BoletoAPI.Models
{
    public class Boleto
    {
        public Guid Id { get; set; }
        public string NomePagador { get; set; }
        public string CpfCnpjPagador { get; set; }
        public string NomeBeneficiario { get; set; }
        public string CpfCnpjBeneficiario { get; set; }
        public decimal Valor { get; set; }
        public DateOnly DataVencimento { get; set; }
        public string? Observacao { get; set; }
        public Guid BancoId { get; set; }

        public Banco Banco { get; set; }
    }
}
