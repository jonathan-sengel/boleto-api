using BoletoAPI.Data;
using BoletoAPI.DTOs;
using BoletoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BoletoAPI.Services
{
    public class BoletoService
    {
        private readonly ApplicationDbContext _context;
        public BoletoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Boleto> ObterPorId(Guid id)
        {
            var boleto = await _context.Boletos
                .Include(b => b.Banco)
                .SingleOrDefaultAsync(b => b.Id == id)
                ?? throw new KeyNotFoundException("Boleto não encontrado com este id;");

            boleto.ProcessarValorAPagar();
            return boleto;
        }

        public async Task<Boleto> Criar(CriarBoletoDto dto)
        {
            var banco = await _context.Bancos
                .FindAsync(dto.BancoId)
                ?? throw new ArgumentException("Banco com id informado não encontrado.");

            var novoBoleto = new Boleto
            {
                NomePagador = dto.NomePagador,
                CpfCnpjPagador = dto.CpfCnpjPagador,
                NomeBeneficiario = dto.NomeBeneficiario,
                CpfCnpjBeneficiario = dto.CpfCnpjBeneficiario,
                Valor = dto.Valor,
                DataVencimento = DateOnly.FromDateTime(dto.DataVencimento),
                Observacao = dto.Observacao,
                BancoId = banco.Id,
            };

            await _context.AddAsync(novoBoleto);
            await _context.SaveChangesAsync();

            return novoBoleto;
        }
    }
}
