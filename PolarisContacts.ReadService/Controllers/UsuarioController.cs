using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService) : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger = logger;
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpPost]
        public async Task<bool> Post()
        {
            try
            {
                await _usuarioService.CreateUserAsync("login", "senha");
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}
