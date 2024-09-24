using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService) : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger = logger;
        private readonly IUsuarioService _usuarioService = usuarioService;

        [HttpGet]
        public async Task<Usuario> GetUserByPasswordAsync(string login, string senha)
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
