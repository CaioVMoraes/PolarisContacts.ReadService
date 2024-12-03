using Microsoft.AspNetCore.Mvc;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("Read/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet("GetUserByPasswordAsync")]
        public async Task<IActionResult> GetUserByPasswordAsync([FromQuery] string login, [FromQuery] string senha)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            {
                return BadRequest("Login e senha são obrigatórios.");
            }

            try
            {
                var usuario = await _usuarioService.GetUserByPasswordAsync(login, senha);
                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet]
        public string Get()
        {
            return "sucesso";
        }
    }
}
