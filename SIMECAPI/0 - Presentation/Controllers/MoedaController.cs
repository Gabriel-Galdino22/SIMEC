using Microsoft.AspNetCore.Mvc;
using SIMECAPI.Models;
using SIMECAPI.Repositories;

namespace SIMECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoedaController : ControllerBase
    {
        private readonly IMoedaRepository _moedaRepository;

        public MoedaController(IMoedaRepository moedaRepository)
        {
            _moedaRepository = moedaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoedas()
        {
            var moedas = await _moedaRepository.GetAllAsync();
            return Ok(moedas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoedaById(int id)
        {
            var moeda = await _moedaRepository.GetByIdAsync(id);
            if (moeda == null)
                return NotFound();

            return Ok(moeda);
        }

        [HttpPost]
        public async Task<IActionResult> AddMoeda([FromBody] Moeda moeda)
        {
            await _moedaRepository.AddAsync(moeda);
            return CreatedAtAction(nameof(GetMoedaById), new { id = moeda.IdMoeda }, moeda);
        }
    }
}
