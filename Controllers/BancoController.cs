using BoletoAPI.DTOs;
using BoletoAPI.Models;
using BoletoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoletoAPI.Controllers
{
    [ApiController]
    [Route("/api/bancos")]
    [ProducesResponseType(typeof(ApiErroDto), StatusCodes.Status400BadRequest)]
    public class BancoController : ControllerBase
    {
        private readonly BancoService _bancoService;
        public BancoController(BancoService bancoService)
        {
            _bancoService = bancoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Banco>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos()
        {
            var bancos = await _bancoService.ObterTodos();
            return Ok(bancos);
        }

        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(Banco), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErroDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterBancoPorCodigo(string codigo)
        {
            var banco = await _bancoService.ObterPorCodigo(codigo);
            if (banco is not null) return Ok(banco);
            return NotFound();
        }

        [HttpPost("novo")]
        [ProducesResponseType(typeof(ResponseIdDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CadastrarBanco(CriarBancoDto bancoDto)
        {
            var novoBanco = await _bancoService.Criar(bancoDto);
            return Created($"/api/bancos/{novoBanco.Id}", new ResponseIdDto { Id = novoBanco.Id });
        }
    }
}
