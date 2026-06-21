using System.ComponentModel.DataAnnotations;

namespace BoletoAPI.DTOs
{
    public class CriarBoletoDto
    {
        [Required]
        public string NomePagador { get; set; }

        [Required]
        [MinLength(11, ErrorMessage = "Tamanho minimo deve ser 11 caracteres")]
        [MaxLength(18, ErrorMessage = "Tamanho máximo deve ser 18 caracteres")]
        public string CpfCnpjPagador { get; set; }

        [Required]
        public string NomeBeneficiario { get; set; }

        [Required]
        [MinLength(11, ErrorMessage = "Tamanho minimo deve ser 11 caracteres")]
        [MaxLength(18, ErrorMessage = "Tamanho máximo deve ser 18 caracteres")]
        public string CpfCnpjBeneficiario { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor minímo deve ser 0,01")]
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        public string? Observacao { get; set; }

        [Required]
        public Guid BancoId { get; set; }
    }
}
