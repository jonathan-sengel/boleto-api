using BoletoAPI.DTOs;
using BoletoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoletoAPI.Controllers
{
    [ApiController]
    [Route("/api/bancos")]
    public class BancoController : ControllerBase
    {
        private readonly BancoService _bancoService;
        public BancoController(BancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var bancos = await _bancoService.ObterTodos();
            return Ok(bancos);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> ObterBancoPorCodigo(string codigo)
        {
            var banco = await _bancoService.ObterPorCodigo(codigo);
            if (banco is not null) return Ok(banco);
            return NotFound();
        }

        [HttpPost("novo")]
        public async Task<IActionResult> CadastrarBanco(CriarBancoDto bancoDto)
        {
            var novoBanco = await _bancoService.Criar(bancoDto);
            return Created($"/api/bancos/{novoBanco.Id}", novoBanco);
        }
    }
}
