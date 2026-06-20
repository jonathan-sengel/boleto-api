using System.ComponentModel.DataAnnotations;

namespace BoletoAPI.DTOs
{
    public class CriarBancoDto
    {
        [Required]
        public string NomeBanco { get; set; }

        [Required]
        public string CodigoBanco { get; set; }

        [Required]
        public decimal PercentualJuros { get; set; }
    }
}
