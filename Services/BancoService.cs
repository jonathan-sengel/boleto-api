using BoletoAPI.Data;
using BoletoAPI.DTOs;
using BoletoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BoletoAPI.Services
{
    public class BancoService
    {
        private readonly ApplicationDbContext _context;
        public BancoService(ApplicationDbContext context) { _context = context; }

        public async Task<List<Banco>> ObterTodos()
            => await _context.Bancos.ToListAsync();

        public async Task<Banco?> ObterPorCodigo(string codigo)
        {
            var banco = await _context.Bancos
                .SingleOrDefaultAsync(bco => bco.CodigoBanco.Equals(codigo));
            return banco;
        }

        public async Task<Banco> Criar(CriarBancoDto dto)
        {
            var existe = await _context.Bancos
                .AnyAsync(b => b.CodigoBanco.Equals(dto.CodigoBanco));

            if (existe)
                throw new ArgumentException("Banco com código informado já existente.");

            var banco = new Banco()
            {
                CodigoBanco = dto.CodigoBanco,
                NomeBanco = dto.NomeBanco,
                PercentualJuros = dto.PercentualJuros,
            };

            await _context.Bancos.AddAsync(banco);
            await _context.SaveChangesAsync();
            return banco;
        }
    }
}
