using Microsoft.AspNetCore.Mvc;
using SIMECAPI.Models;
using SIMECAPI.Repositories;
using System.Threading.Tasks;

namespace SIMECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DicaSustentabilidadeController : ControllerBase
    {
        private readonly IDicaSustentabilidadeRepository _dicaSustentabilidadeRepository;

        public DicaSustentabilidadeController(IDicaSustentabilidadeRepository dicaSustentabilidadeRepository)
        {
            _dicaSustentabilidadeRepository = dicaSustentabilidadeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDicas()
        {
            var dicas = await _dicaSustentabilidadeRepository.GetAllAsync();
            return Ok(dicas);
        }

        [HttpPost]
        public async Task<IActionResult> AddDica([FromBody] DicaSustentabilidade dica)
        {
            await _dicaSustentabilidadeRepository.AddAsync(dica);
            return CreatedAtAction(nameof(GetAllDicas), new { id = dica.IdDica }, dica);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDicaById(int id)
        {
            var dica = await _dicaSustentabilidadeRepository.GetByIdAsync(id);
            if (dica == null)
                return NotFound();

            return Ok(dica);
        }
    }
}
