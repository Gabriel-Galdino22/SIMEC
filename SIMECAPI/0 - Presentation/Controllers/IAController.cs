using Microsoft.AspNetCore.Mvc;
using SIMECAPI.Models;
using SIMECAPI.Repositories;
using SIMECAPI.Services;

namespace SIMECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IAController : ControllerBase
    {
        private readonly IConsumoEnergiaRepository _consumoEnergiaRepository;
        private readonly IIARecomendacaoService _iaService;

        public IAController(IConsumoEnergiaRepository consumoEnergiaRepository, IIARecomendacaoService iaService)
        {
            _consumoEnergiaRepository = consumoEnergiaRepository;
            _iaService = iaService;
        }

        [HttpPost("Treinar")]
        public async Task<IActionResult> TreinarModelo()
        {
            // Obtém os dados de consumo
            var consumos = (await _consumoEnergiaRepository.GetAllAsync()).ToList();
            if (consumos.Count == 0)
                return BadRequest("Nenhum dado de consumo disponível para treinamento.");

            // Treina o modelo
            _iaService.TreinarModelo(consumos);
            return Ok("Modelo treinado com sucesso.");
        }

        [HttpPost("Recomendar")]
        public IActionResult GerarRecomendacao([FromBody] ConsumoEnergia consumoAtual)
        {
            // Gera a recomendação com base no consumo atual
            var recomendacao = _iaService.GerarRecomendacao(consumoAtual);
            return Ok(recomendacao);
        }
    }
}
