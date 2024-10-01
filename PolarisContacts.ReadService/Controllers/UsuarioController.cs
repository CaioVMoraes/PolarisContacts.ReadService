using Microsoft.AspNetCore.Mvc;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.ReadService.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService) : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger = logger;
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpGet("GetUserByPasswordAsync")]
        public async Task<Usuario> GetUserByPasswordAsync([FromQuery] string login, [FromQuery] string senha)
        {
            try
            {
                return await _usuarioService.GetUserByPasswordAsync(login, senha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
