using Microsoft.AspNetCore.Mvc;
using SIMECAPI.Models;
using SIMECAPI.Repositories;

namespace SIMECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartamentoController : ControllerBase
    {
        private readonly IApartamentoRepository _apartamentoRepository;

        public ApartamentoController(IApartamentoRepository apartamentoRepository)
        {
            _apartamentoRepository = apartamentoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApartamentos()
        {
            var apartamentos = await _apartamentoRepository.GetAllAsync();
            return Ok(apartamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartamentoById(int id)
        {
            var apartamento = await _apartamentoRepository.GetByIdAsync(id);
            if (apartamento == null)
                return NotFound();

            return Ok(apartamento);
        }

        [HttpPost]
        public async Task<IActionResult> AddApartamento([FromBody] Apartamento apartamento)
        {
            await _apartamentoRepository.AddAsync(apartamento);
            return CreatedAtAction(nameof(GetApartamentoById), new { id = apartamento.IdApartamento }, apartamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartamento(int id, [FromBody] Apartamento apartamento)
        {
            if (id != apartamento.IdApartamento)
                return BadRequest();

            await _apartamentoRepository.UpdateAsync(apartamento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartamento(int id)
        {
            await _apartamentoRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
