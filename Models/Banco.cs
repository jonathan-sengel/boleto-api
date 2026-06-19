namespace BoletoAPI.Models
{
    public class Banco
    {
        public Guid Id { get; set; } = new Guid();
        public string NomeBanco { get; set; }
        public string CodigoBanco { get; set; }
        public decimal PercentualJuros { get; set; }
    }
}
