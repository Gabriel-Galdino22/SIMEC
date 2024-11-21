using Microsoft.AspNetCore.Mvc;
using SIMECAPI.Models;
using SIMECAPI.Repositories;

namespace SIMECAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.IdUsuario }, usuario);
        }
    }
}
