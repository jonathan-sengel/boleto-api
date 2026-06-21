using BoletoAPI.DTOs;
using BoletoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoletoAPI.Controllers
{
    [ApiController]
    [Route("/api/boletos")]
    public class BoletoController : ControllerBase
    {

        private readonly BoletoService _service;
        public BoletoController(BoletoService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterBoletoAtualizado(Guid id)
        {
            var boletoAtualizado = await _service.ObterPorId(id);
            return Ok(boletoAtualizado);
        }

        [HttpPost("novo")]
        public async Task<IActionResult> CadastrarBoleto(CriarBoletoDto boletoDto)
        {
            if (boletoDto.DataVencimento <= DateTime.Now)
                return BadRequest("Data de vencimento deve ser futura.");

            var boleto = await _service.Criar(boletoDto);

            return Created($"/api/boletos/{boleto.Id}", new EntidadeCriadaDto { Id = boleto.Id });
        }
    }
}
