using Microsoft.AspNetCore.Mvc;
using SIMECAPI.Models;
using SIMECAPI.Repositories;

namespace SIMECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoEnergiaController : ControllerBase
    {
        private readonly IConsumoEnergiaRepository _consumoEnergiaRepository;

        public ConsumoEnergiaController(IConsumoEnergiaRepository consumoEnergiaRepository)
        {
            _consumoEnergiaRepository = consumoEnergiaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConsumos()
        {
            var consumos = await _consumoEnergiaRepository.GetAllAsync();
            return Ok(consumos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsumoById(int id)
        {
            var consumo = await _consumoEnergiaRepository.GetByIdAsync(id);
            if (consumo == null)
                return NotFound();

            return Ok(consumo);
        }

        [HttpPost]
        public async Task<IActionResult> AddConsumo([FromBody] ConsumoEnergia consumo)
        {
            await _consumoEnergiaRepository.AddAsync(consumo);
            return CreatedAtAction(nameof(GetConsumoById), new { id = consumo.IdConsumo }, consumo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumo(int id, [FromBody] ConsumoEnergia consumo)
        {
            if (id != consumo.IdConsumo)
                return BadRequest();

            await _consumoEnergiaRepository.UpdateAsync(consumo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumo(int id)
        {
            await _consumoEnergiaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
